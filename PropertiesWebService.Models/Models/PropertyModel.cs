
using PropertiesWebService.Models.Models.Base;

using System.ComponentModel.DataAnnotations;

namespace PropertiesWebService.Models.Models
{
    public class PropertyModel : ModelBase
    {
        [Required]
        [MaxLength(1024, ErrorMessage = "Address length cannot exceed 1024 characters")]
        public string Address { get; set; } = null!;

        [MaxLength(8192, ErrorMessage = "Description length cannot exceed 8192 characters")]
        public string Description { get; set; } = null!;

        public string TypeName { get; set; } = null!;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "TypeId must be a positive integer.")]
        public int TypeId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        public IList<SpaceModel> Spaces { get; set; } = [];
    }
}
