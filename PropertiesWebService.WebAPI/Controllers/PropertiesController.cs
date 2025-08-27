using Microsoft.AspNetCore.Mvc;

using PropertiesWebService.Models.Models;
using PropertiesWebService.Models.SearchCriteria;
using PropertiesWebService.Services.Interfaces;
using PropertiesWebService.WebAPI.Controllers.Base;

using System.ComponentModel;

namespace PropertiesWebService.WebAPI.Controllers
{
    public class PropertiesController(ISearchService<PropertyModel, PropertySearchCriteria> searchService) : BaseAppController
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync(
            [FromQuery] PropertySearchCriteria criteria,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string sortMember = null!,
            [FromQuery] ListSortDirection sortDirection = ListSortDirection.Ascending)
        {
            var query = new Query<PropertySearchCriteria>
            {
                SearchCriteria = criteria,
                Page = page,
                PageSize = pageSize,
                SortDirection = sortDirection,
                SortMember = sortMember
            };

            var result = await searchService.GetAsync(query);
            return Ok(result);
        }

        [HttpGet()]
        [Route("{id:int:min(1)}")]
        public async Task<IActionResult> GetAsync([FromRoute] int id)
        {
            var property = await searchService.GetAsync(id);
            if (property is null)
            {
                return NotFound();
            }
            return Ok(property);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] PropertyModel model)
        {
            if (ModelState.IsValid)
            {
                var item = await searchService.AddAsync(model);
                return CreatedAtAction(nameof(GetAsync), new { id = item.Id }, item);
            }
            return BadRequest();
        }

        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] PropertyModel model)
        {
            var updated = await searchService.UpdateAsync(id, model);
            if (updated is null)
                return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleted = await searchService.DeleteAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}
