using FluentResults;
using ApiSample.Domain.Common.FluentResult;

namespace ApiSample.Api.Common.Paging
{
    public class PagedQuery
    {
        public int Page { get; set; } = 1;
        public int? PageSize { get; set; } = null;
        public string OrderBy { get; set; } = null;
        public bool OrderByDesc { get; set; } = false;
        public string FilterField { get; set; } = null;
        public string FilterText { get; set; } = null;

        public bool HasSearch => FilterField != null && FilterText != null;

        public Result IsValid()
        {
            if (FilterField != null && FilterText is null)
            {
                return ResultFactory.Error("FilterText", $"{nameof(FilterText)} is required.");
            }

            return Result.Ok();
        }
    }
}
