using Microsoft.AspNetCore.Mvc;
using MyInventory.Logic;

namespace MyInventory.Service
{
    [Route("local-images")]
    public class LocalImageController : Controller
    {
        private readonly InventoryLogic _inventoryLogic;

        public LocalImageController(InventoryLogic inventoryLogic)
        {
            _inventoryLogic = inventoryLogic;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string path)
        {
            if (string.IsNullOrWhiteSpace(path) || !System.IO.File.Exists(path)) return NotFound();

            var mimeType = _inventoryLogic.GetMimeType(path);
            return PhysicalFile(path, mimeType);
        }
    }
}
