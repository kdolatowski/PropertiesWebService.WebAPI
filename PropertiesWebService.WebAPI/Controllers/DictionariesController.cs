using Microsoft.AspNetCore.Mvc;

using PropertiesWebService.Services.Interfaces;
using PropertiesWebService.WebAPI.Controllers.Base;

namespace PropertiesWebService.WebAPI.Controllers
{
    public class DictionariesController(IDictionariesService dictionariesService) : BaseAppController
    {
        [HttpGet("{dictionaryName}")]
        public async Task<IActionResult> GetEntries([FromRoute] string dictionaryName)
        {
            try 
            {
                var entries = await dictionariesService.GetAsync(dictionaryName);
                return Ok(entries);
            }
            catch (NotSupportedException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
