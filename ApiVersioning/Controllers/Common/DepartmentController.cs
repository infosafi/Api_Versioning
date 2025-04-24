using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ApiVersioning.Controllers.Common
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiVersion("3.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    public class DepartmentController : Controller
    {

        [HttpGet("get-department")]
        public async Task<IActionResult> GetDepartment()
        {
            return  new OkObjectResult($"Department share for This all Version ");
        }


        [MapToApiVersion("1.0")]
        [HttpGet("get-location")]
        public async Task<IActionResult> GetLocation()
        {
            return new OkObjectResult($"Department share for Only Version 1");
        }
    }
}
