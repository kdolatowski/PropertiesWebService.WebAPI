
using PropertiesWebService.Models.Models.Base;

using System.ComponentModel.DataAnnotations;

namespace PropertiesWebService.Models.Models
{
    public class SpaceModel : ModelBase
    {
        public int PropertyId { get; set; }

        [MaxLength(8192, ErrorMessage = "Description length cannot exceed 8192 characters")]
        public string? Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Size must be greater than zero.")]    
        public decimal Size { get; set; }

        public string? SpaceType { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "TypeId must be a positive integer.")]
        public int TypeId { get; set; }

    }
}
