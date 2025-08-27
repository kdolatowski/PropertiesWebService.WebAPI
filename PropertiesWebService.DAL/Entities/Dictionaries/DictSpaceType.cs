using Microsoft.EntityFrameworkCore;

using PropertiesWebService.DAL.Entities;

using PropertiesWebService.DAL.Entities.Base;

namespace PropertiesWebService.DAL.Entities.Dictionaries
{
    [Index(nameof(Name), Name = "IX_DictSpaceType_Name")]
    public class DictSpaceType : DictionaryBase
    {
        public virtual ICollection<Space> Spaces { get; set; } = [];
    }
}
