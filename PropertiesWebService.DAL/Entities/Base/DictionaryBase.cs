using System.ComponentModel.DataAnnotations;
using PropertiesWebService.DAL.Entities.Base;

namespace PropertiesWebService.DAL.Entities.Base
{
    public abstract class DictionaryBase : EntityBase
    {
        [MaxLength(100)]
        public required string Name { get; set; } = null!;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; } = null;
    }
}
