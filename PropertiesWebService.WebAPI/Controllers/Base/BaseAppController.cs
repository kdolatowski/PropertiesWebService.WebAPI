using Microsoft.AspNetCore.Mvc;

namespace PropertiesWebService.WebAPI.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseAppController : Controller
    {
    }
}
