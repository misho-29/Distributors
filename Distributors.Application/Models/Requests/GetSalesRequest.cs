using Distributors.Core.DisplayTools.Filter.Models;
using Distributors.Core.DisplayTools.Pagination;

namespace Distributors.Application.Models.Requests;
public class GetSalesRequest
{
    public PaginationObj Pagination { get; set; } = null!;
    public Filter? Filter { get; set; }
}
