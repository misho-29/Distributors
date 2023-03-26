namespace Distributors.Core.Exceptions;
public enum ExceptionCode
{
    GeneralValidationException,
    DistributorNotFound,
    RecommenderNotExists,
    RecommenderExceededRecommendationCount,
    RecommendationDepthExceeded,
    ProductCodeAlreadyExists,
    ProductNotFound,
    UnableToFilterBySpecifiedFiled,
    OperatorCanNotBeAppliedToSpecifiedField,
    PropertyNotExists,
    PropertyTypeNotFound,
    DistributorIsRecommender
}
