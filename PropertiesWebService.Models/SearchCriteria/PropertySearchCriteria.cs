namespace PropertiesWebService.Models.SearchCriteria
{
    public class PropertySearchCriteria
    {
        public int? PropertyTypeId { get; set; }

        public decimal? PriceMin { get; set; }

        public decimal? PriceMax { get; set; }

        public string? Address { get; set; }

    }
}
