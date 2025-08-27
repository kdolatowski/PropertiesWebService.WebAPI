using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using PropertiesWebService.DAL.Entities;
using PropertiesWebService.DAL.Entities.Base;
using PropertiesWebService.DAL.Entities.Dictionaries;

namespace PropertiesWebService.DAL.Entities
{
    [Index(nameof(PropertyId), Name = "IX_Space_PropertyId")]
    [Index(nameof(TypeId), Name = "IX_Space_TypeId")]
    [Index(nameof(PropertyId), nameof(TypeId), Name = "IX_Space_PropertyId_TypeId")]
    [Index(nameof(PropertyId), nameof(Size), Name = "IX_Space_PropertyId_Size")]
    [Index(nameof(TypeId), nameof(Size), Name = "IX_Space_TypeId_Size")]
    public class Space : EntityBase
    {
        [Precision(18, 2)]
        public required decimal Size { get; set; }

        [MaxLength(8192)]
        public string? Description { get; set; }

        public required int PropertyId { get; set; }

        public virtual Property Property { get; set; } = null!;

        public required int TypeId { get; set; }

        public virtual DictSpaceType Type { get; set; } = null!;
    }
}
