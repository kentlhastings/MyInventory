using Microsoft.AspNetCore.Mvc;
using MyInventory.Logic;
using MyInventory.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

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
            var records = await _inventoryLogic.GetCollections();
            return Ok(records);
        }

        [HttpGet("{collectionId}/value")]
        public IActionResult GetCollectionValue(Guid collectionId)
        {
            var collection = _inventoryLogic.GetCollectionValue(collectionId);
            if (collection != null) return Ok(collection);
            return NotFound();
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

        [HttpPost("files/open")]
        public IActionResult Post([FromBody] FileRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Path) || !System.IO.File.Exists(request.Path)) return BadRequest("Invalid file path");
            
            var args = $"/select,\"{request.Path}\"";
            Process.Start("explorer.exe", args);
            
            return Ok();
        }

        [HttpPost("image/{collectionId}")]
        public async Task<IActionResult> GetImage([FromBody] Record record, Guid collectionId)
        {
            if (record is null) return BadRequest();

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

                if (record.Images is null) record.Images = new List<string>();
                record.Images.Add(selectedPath);

                await _inventoryLogic.UpdateRecord(collectionId, record);

                return Ok(new FileRequest
                {
                    Path = selectedPath
                });
            }

            return NoContent();
        }
    }
}
