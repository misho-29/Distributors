using AutoMapper;
using X.PagedList;

namespace Distributors.Application.DisplayTools.Pagination;
public static class PaginationExtensions
{
    public static PaginatedObject<TDestination> ToPaginatedObject<TSource, TDestination>(this IPagedList<TSource> pagedList, IMapper mapper)
    {
        var pagedListToList = pagedList.ToList();

        var paginatedObject = new PaginatedObject<TDestination>()
        {
            PageNumber = pagedList.PageNumber,
            CurrentPageSize = pagedList.Count,
            PageCount = pagedList.PageCount,
            TotalItemCount = pagedList.TotalItemCount,
            Data = mapper.Map<IEnumerable<TDestination>>(pagedListToList),
        };

        return paginatedObject;
    }
}