using Microsoft.EntityFrameworkCore;

using PropertiesWebService.DAL.Entities.Base;

namespace PropertiesWebService.DAL.Entities.Dictionaries
{
    [Index(nameof(Name), Name = "IX_DictPropertyType_Name")]
    public class DictPropertyType : DictionaryBase
    {
        public virtual ICollection<Property> Properties { get; set; } = [];
    }
}
