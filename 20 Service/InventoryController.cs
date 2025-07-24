using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyInventory.Logic;
using MyInventory.Models;

namespace MyInventory.Service
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryLogic _inventoryLogic;

        public InventoryController(InventoryLogic inventoryLogic)
        {
            _inventoryLogic = inventoryLogic;
        }

        [HttpGet]
        public async Task<IActionResult> GetCollections()
        {
            var records = _inventoryLogic.GetTestCollections().Result;
            return Ok(records);
        }

        [HttpPost("{collectionId}/records/new")]
        public async Task<IActionResult> InsertRecord(Guid collectionId, [FromBody] Record record)
        {
            var createdRecord = await _inventoryLogic.CreateRecord(collectionId, record);
            return Ok(createdRecord);
        }

        [HttpPut("{collectionId}/records/update")]
        public async Task<IActionResult> UpdateRecord(Guid collectionId, [FromBody] Record record)
        {
            var updatedRecord = await _inventoryLogic.UpdateRecord(collectionId, record);
            return Ok(updatedRecord);
        }

        [HttpDelete("{collectionId}/records/delete/{recordId}")]
        public async Task<IActionResult> DeleteRecord(Guid collectionId, Guid recordId)
        {
            var deletedRecordId = await _inventoryLogic.DeleteRecord(collectionId, recordId);
            return Ok(deletedRecordId);
        }

        [HttpPost("collections/new")]
        public async Task<IActionResult> InsertCollection([FromBody] Collection collection)
        {
            var createdCollection = await _inventoryLogic.CreateCollection(collection);
            return Ok(createdCollection);
        }

        [HttpPut("collections/update")]
        public async Task<IActionResult> UpdateCollection([FromBody] Collection collection)
        {
            var updatedCollection = await _inventoryLogic.UpdateCollection(collection);
            return Ok(updatedCollection);
        }

        [HttpDelete("collections/delete/{collectionId}")]
        public async Task<IActionResult> DeleteCollection(Guid collectionId)
        {
            var deletedCollectionId = await _inventoryLogic.DeleteCollection(collectionId);
            return Ok(deletedCollectionId);
        }

        [HttpPost("error")]
        public async Task<IActionResult> ReportError([FromBody] Error error)
        {
            var reportedError = await _inventoryLogic.ReportError(error);
            return Ok(reportedError);
        }

        [HttpGet("image")]
        public IActionResult Get([FromQuery] string path)
        {
            if (string.IsNullOrWhiteSpace(path) || !System.IO.File.Exists(path)) return NotFound();

            var mimeType = _inventoryLogic.GetMimeType(path);
            return PhysicalFile(path, mimeType);
        }

        [HttpGet("files/get")]
        public IActionResult GetImage()
        {
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Title = "Select an image",
                Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png",
                Multiselect = false
            };

            var result = dialog.ShowDialog();
            if (result == true)
            {
                var selectedPath = dialog.FileName;
                return Ok(new FileRequest 
                { 
                    Path = selectedPath 
                });
            }

            return NoContent();
        }

        [HttpPost("files/open")]
        public IActionResult Post([FromBody] FileRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Path) || !System.IO.File.Exists(request.Path)) return BadRequest("Invalid file path");
            
            var args = $"/select,\"{request.Path}\"";
            Process.Start("explorer.exe", args);
            
            return Ok();
        }
    }
}
