namespace PropertiesWebService.Models.SearchCriteria
{
    public class SpaceSearchCriteria
    {
        public int? PropertyId { get; set; }

        public  decimal? SizeMin { get; set; }

        public decimal? SizeMax { get; set; }

        public int? SpaceTypeId { get; set; }

    }
}
