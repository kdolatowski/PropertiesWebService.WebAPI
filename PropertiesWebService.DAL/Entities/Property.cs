using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using PropertiesWebService.DAL.Entities;
using PropertiesWebService.DAL.Entities.Base;
using PropertiesWebService.DAL.Entities.Dictionaries;

namespace PropertiesWebService.DAL.Entities
{
    [Index(nameof(Address), Name = "IX_Property_Address")]
    [Index(nameof(TypeId), Name = "IX_Property_TypeId")]
    [Index(nameof(Price), Name = "IX_Property_Price")]
    [Index(nameof(TypeId), nameof(Address), Name = "IX_Property_TypeId_Address")]
    [Index(nameof(TypeId), nameof(Price), Name = "IX_Property_TypeId_Price")]
    [Index(nameof(Address), nameof(Price), Name = "IX_Property_Address_Price")]
    public class Property : EntityBase
    {
        [MaxLength(1024)]
        public required string Address { get; set; } = null!;

        public required int TypeId { get; set; }

        public virtual DictPropertyType Type { get; set; } = null!;

        [Precision(18, 2)]
        public required decimal Price { get; set; }

        [MaxLength(8192)]
        public string? Description { get; set; }

        public virtual ICollection<Space> Spaces { get; set; } = [];
    }
}
