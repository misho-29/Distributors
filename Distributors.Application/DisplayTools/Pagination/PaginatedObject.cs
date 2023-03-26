namespace Distributors.Application.DisplayTools.Pagination;
public class PaginatedObject<T>
{
    public int PageNumber { get; set; }
    public int CurrentPageSize { get; set; }
    public int PageCount { get; set; }
    public int TotalItemCount { get; set; }
    public IEnumerable<T>? Data { get; set; }
}
