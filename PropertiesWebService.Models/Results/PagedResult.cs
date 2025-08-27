using PropertiesWebService.Models.Results;

namespace PropertiesWebService.Models.Results
{
    public class PagedResult<TEntity>
    {
        public ICollection<TEntity> Results { get; set; } = [];

        public int Page { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public int PagesCount
        {
            get
            {
                return PageSize != 0 ? Convert.ToInt32(Math.Ceiling(Convert.ToDouble(TotalCount) / Convert.ToDouble(PageSize))) : 0;
            }
        }

        public PagedResult<T> CreateNew<T>(ICollection<T> list)
        {
            return new PagedResult<T>
            {
                Results = list,
                Page = Page,
                PageSize = PageSize,
                TotalCount = TotalCount,
            };
        }
    }
}
