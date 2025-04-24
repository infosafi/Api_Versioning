using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ApiVersioning.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]

    public class EmployeeController : Controller
    {
        [HttpGet("get-employee")]
        public IActionResult getEmployeeName()
        {
            return new OkObjectResult("Provide Employee Name for Version 1");
        }
    }
}
