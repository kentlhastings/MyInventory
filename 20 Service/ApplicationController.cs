using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MyInventory.Logic;

namespace MyInventory.Service
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationController : ControllerBase
    {
        private readonly ApplicationLogic _applicationLogic;

        public ApplicationController(ApplicationLogic applicationLogic)
        {
            _applicationLogic = applicationLogic;
        }

        [HttpGet("load")]
        public async Task<IActionResult> Load()
        {
            var data = await _applicationLogic.LoadData();
            return Ok(data);
        }
    }
}
