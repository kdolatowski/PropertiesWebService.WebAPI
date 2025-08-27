using Microsoft.AspNetCore.Mvc;

using PropertiesWebService.Models.Models;
using PropertiesWebService.Models.SearchCriteria;
using PropertiesWebService.Services.Interfaces;
using PropertiesWebService.WebAPI.Controllers.Base;

using System.ComponentModel;

namespace PropertiesWebService.WebAPI.Controllers
{
    public class SpacesController(ISearchService<SpaceModel, SpaceSearchCriteria> searchService) : BaseAppController
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync(
            [FromQuery] SpaceSearchCriteria criteria,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string? sortMember = null,
            [FromQuery] ListSortDirection sortDirection = ListSortDirection.Ascending)
        {
            var query = new Query<SpaceSearchCriteria>
            {
                SearchCriteria = criteria,
                Page = page,
                PageSize = pageSize,
                SortDirection = sortDirection
            };

            var result = await searchService.GetAsync(query);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var space = await searchService.GetAsync(id);
            if (space is null)
                return NotFound();
            return Ok(space);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] SpaceModel model)
        {
            var created = await searchService.AddAsync(model);
            return CreatedAtAction(nameof(GetAsync), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] SpaceModel model)
        {
            var updated = await searchService.UpdateAsync(id, model);
            if (updated is null)
                return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deleted = await searchService.DeleteAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}
