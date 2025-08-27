using PropertiesWebService.Models.Models.Base;

namespace PropertiesWebService.Services.Interfaces
{
    public interface IDictionariesService
    {
        Task<IList<DictionaryModelBase>> GetAsync(string dictionaryName, bool includeInactive = false);
    }
}
