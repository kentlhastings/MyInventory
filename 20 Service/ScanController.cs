using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyInventory.Logic;

namespace MyInventory.Service
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScanController : ControllerBase
    {
        private readonly ScanLogic _scanLogic;

        public ScanController(ScanLogic scanLogic)
        {
            _scanLogic = scanLogic;
        }

        [HttpGet("scan")]
        public async Task<IActionResult> GetScanResults()
        {
            var results = await _scanLogic.GetScanResultsAsync();
            return Ok(results);
        }
    }
}
