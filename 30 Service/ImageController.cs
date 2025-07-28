using MyInventory.Logic;
using Microsoft.AspNetCore.Mvc;

namespace MyInventory.Service
{
    [Route("local-images")]
    public class LocalImageController : Controller
    {
        private readonly ImageLogic _imageLogic;

        public LocalImageController(ImageLogic imageLogic)
        {
            _imageLogic = imageLogic;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string path)
        {
            if (string.IsNullOrWhiteSpace(path) || !System.IO.File.Exists(path)) return NotFound();

            var mimeType = _imageLogic.GetMimeType(path);
            return PhysicalFile(path, mimeType);
        }
    }
}
