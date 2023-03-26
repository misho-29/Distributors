using Distributors.Core.DisplayTools.Filter.Models;
using Distributors.Core.Exceptions;
using System.Linq.Dynamic.Core;
using System.Reflection;
using Type = Distributors.Core.DisplayTools.Filter.Models.Type;

namespace Distributors.Core.DisplayTools.Filter;

public static class FilterBuilder
{
    /// <summary>
    /// Builds query string for passed filterRules
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    /// <remarks>
    /// Operator.In and Operator.Equal work with multiple values. all other operators work with only single value
    /// </remarks>
    public static IQueryable<T> BuildFilterQuery<T>(this IQueryable<T> queryable, Models.Filter? filter)
        where T : class
    {
        if (filter?.FilterRules is null)
        {
            return queryable;
        }

        var filterRules = filter.FilterRules;

        // Build condition

        var condition = "";

        foreach (var filterRule in filterRules)
        {
            var currentCondition = filterRule.Operator switch
            {
                Operator.Contain => // Can be applied to string. Works with multiple values
                    CreateContainCondition<T>(filterRule),
                Operator.GreaterOrEqual => // Can be applied to number, datetime. Works with single value
                    CreateGreaterOrEqualCondition<T>(filterRule),
                Operator.LessOrEqual => // Can be applied to number, datetime. Works with single value
                    CreateLessOrEqualCondition<T>(filterRule),
                Operator.Equal => // Can be applied to string, number, enum, boolean, datetime. Works with multiple values
                    CreateEqualCondition<T>(filterRule),
                Operator.In => // Can be applied to array of strings, numbers, enums, booleans, dateTimes. Works with multiple values
                    CreateInCondition<T>(filterRule),
                _ => "",
            };

            if (filterRules.IndexOf(filterRule) != 0)
            {
                condition += " && ";
            }

            condition += currentCondition;
        }

        return !string.IsNullOrEmpty(condition) ? queryable.Where(condition) : queryable;
    }

    public static IEnumerable<T> BuildFilterQuery<T>(this IEnumerable<T> enumerable, Models.Filter? filter)
    where T : class
    {
        return enumerable.AsQueryable().BuildFilterQuery(filter);
    }

    private static string CreateContainCondition<T>(FilterRule filterRule)
        where T : class
    {
        var values = filterRule.Value.Split(',');
        var fieldType = GetPropertyType<T>(filterRule.Field);

        var condition = "(";

        for (var index = 0; index < values.Length; index++)
        {
            if (index != 0)
            {
                condition += " || ";
            }

            if (fieldType == Type.String)
            {
                condition += $"{filterRule.Field}.ToLower().Contains(\"{values[index].ToLower()}\")";
            }
            else
            {
                throw IrrelevantOperatorAndFieldException(filterRule.Operator, filterRule.Field);
            }
        }
        condition += ")";

        return condition;
    }

    private static string CreateGreaterOrEqualCondition<T>(FilterRule filterRule)
        where T : class
    {
        var value = filterRule.Value;
        var fieldType = GetPropertyType<T>(filterRule.Field);

        var condition = fieldType switch
        {
            Type.Number => $"{filterRule.Field} >= {value}",
            Type.DateTime => $"{filterRule.Field} >= {value.ConvertToDateTimeRepresentation()}",
            _ => throw IrrelevantOperatorAndFieldException(filterRule.Operator, filterRule.Field),
        };

        return condition;
    }

    private static string CreateLessOrEqualCondition<T>(FilterRule filterRule)
        where T : class
    {
        var value = filterRule.Value;
        var fieldType = GetPropertyType<T>(filterRule.Field);

        var condition = fieldType switch
        {
            Type.Number => $"{filterRule.Field} <= {value}",
            Type.DateTime => $"{filterRule.Field} <= {value.ConvertToDateTimeRepresentation()}",
            _ => throw IrrelevantOperatorAndFieldException(filterRule.Operator, filterRule.Field),
        };

        return condition;
    }

    private static string CreateEqualCondition<T>(FilterRule filterRule)
        where T : class
    {
        var values = filterRule.Value.Split(',');
        var fieldType = GetPropertyType<T>(filterRule.Field);

        var condition = "(";

        for (var index = 0; index < values.Length; index++)
        {
            if (index != 0)
            {
                condition += " || ";
            }

            condition += fieldType switch
            {
                Type.String => $"{filterRule.Field}.ToLower() == \"{values[index].ToLower()}\"",
                Type.Enum => $"{filterRule.Field} == \"{values[index]}\"",
                Type.DateTime => $"{filterRule.Field} == {values[index].ConvertToDateTimeRepresentation()}",
                Type.Boolean => $"{filterRule.Field} == {values[index]}",
                Type.Number => $"{filterRule.Field} == {values[index]}",
                _ => throw IrrelevantOperatorAndFieldException(filterRule.Operator, filterRule.Field),
            };
        }
        condition += ")";

        return condition;
    }

    private static string CreateInCondition<T>(FilterRule filterRule)
        where T : class
    {
        var values = filterRule.Value.Split(',');
        var fieldType = GetPropertyType<T>(filterRule.Field);

        var condition = "(";

        for (var index = 0; index < values.Length; index++)
        {
            if (index != 0)
            {
                condition += " || ";
            }

            condition += fieldType switch
            {
                Type.StringArray => $"{filterRule.Field}.Contains(\"{values[index]}\")",
                Type.EnumArray => $"{filterRule.Field}.Contains(\"{values[index]}\")",
                Type.DateTimeArray => $"{filterRule.Field}.Contains({values[index].ConvertToDateTimeRepresentation()})",
                Type.BooleanArray => $"{filterRule.Field}.Contains({values[index]})",
                Type.NumberArray => $"{filterRule.Field}.Contains({values[index]})",
                _ => throw IrrelevantOperatorAndFieldException(filterRule.Operator, filterRule.Field),
            };
        }
        condition += ")";

        return condition;
    }

    private static string ConvertToDateTimeRepresentation(this string value)
    {
        var dateTime = DateTime.Parse(value);
        var dateTimeRepresentation = $"DateTime({dateTime.Year}, {dateTime.Month}, {dateTime.Day}, " +
            $"{dateTime.Hour}, {dateTime.Minute}, {dateTime.Second}, {dateTime.Millisecond})";

        return dateTimeRepresentation;
    }

    private static ValidationException IrrelevantOperatorAndFieldException(Operator @operator, string field)
    {
        return new ValidationException(ExceptionCode.OperatorCanNotBeAppliedToSpecifiedField, $"Operator: {@operator}, Field: {field}");
    }

    private static Type GetPropertyType<T>(string propertyName)
    where T : class
    {
        var propertyInfo = GetPropertInfoByPropertyName<T>(propertyName);
        if (propertyInfo is null)
        {
            throw new ValidationException(ExceptionCode.PropertyNotExists, $"Property: {propertyName}");
        }

        if (propertyInfo.PropertyType == typeof(string))
        {
            return Type.String;
        }

        if (propertyInfo.PropertyType.IsEnum)
        {
            return Type.Enum;
        }

        if (propertyInfo.PropertyType == typeof(bool) || propertyInfo.PropertyType == typeof(bool?))
        {
            return Type.Boolean;
        }

        var numericTypes = new List<System.Type> {
            typeof(int),
            typeof(int?),
            typeof(double),
            typeof(double?),
            typeof(float), typeof(float?),
            typeof(decimal),
            typeof(decimal?)
        };

        if (numericTypes.Contains(propertyInfo.PropertyType))
        {
            return Type.Number;
        }

        if (propertyInfo.PropertyType == typeof(DateTime) || propertyInfo.PropertyType == typeof(DateTime?))
        {
            return Type.DateTime;
        }

        if (new List<System.Type> { typeof(IEnumerable<string>), typeof(List<string>) }.Contains(propertyInfo.PropertyType))
        {
            return Type.StringArray;
        }

        if (new List<System.Type> { typeof(IEnumerable<Enum>), typeof(List<Enum>) }.Contains(propertyInfo.PropertyType))
        {
            return Type.EnumArray;
        }

        var numericArrayTypes = new List<System.Type> {
            typeof(IEnumerable<int>),
            typeof(List<int>),
            typeof(IEnumerable<double>),
            typeof(List<double>),
            typeof(IEnumerable<float>),
            typeof(List<float>),
            typeof(IEnumerable<decimal>),
            typeof(List<decimal>)
        };

        if (numericArrayTypes.Contains(propertyInfo.PropertyType))
        {
            return Type.NumberArray;
        }

        if (propertyInfo.PropertyType == typeof(IEnumerable<DateTime>) || propertyInfo.PropertyType == typeof(List<DateTime>))
        {
            return Type.DateTimeArray;
        }

        throw new FilterException(ExceptionCode.PropertyTypeNotFound);
    }

    private static PropertyInfo? GetPropertInfoByPropertyName<T>(string propertyName)
    {
        var propertyParts = propertyName.Split('.');
        System.Type type = typeof(T);
        PropertyInfo? propertyInfo = null;
        foreach (var propertyPart in propertyParts)
        {
            propertyInfo = type.GetProperties().FirstOrDefault(property => property.Name.ToLower() == propertyPart.ToLower());
            if (propertyInfo is null)
            {
                return null;
            }
            type = propertyInfo.PropertyType;
        }

        return propertyInfo;
    }
}
