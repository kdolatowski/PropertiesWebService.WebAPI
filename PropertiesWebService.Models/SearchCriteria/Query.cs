using System.ComponentModel;

namespace PropertiesWebService.Models.SearchCriteria
{
    public class Query<T>
    {
        public T SearchCriteria { get; set; } = default!;

        public int Page { get; set; }

        public int PageSize { get; set; }

        public string SortMember { get; set; } = null!;

        public ListSortDirection SortDirection { get; set; }
    }
}
