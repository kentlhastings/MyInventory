using System;
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

        [HttpGet("create")]
        public async Task<IActionResult> Create(Record record)
        {
            var createdRecord = await _inventoryLogic.CreateRecord(record);
            return Ok(createdRecord);
        }

        [HttpGet("update")]
        public async Task<IActionResult> Update(Record record)
        {
            var updatedRecord = await _inventoryLogic.UpdateRecord(record);
            return Ok(updatedRecord);
        }

        [HttpGet("delete")]
        public async Task<IActionResult> Delete(Guid recordId)
        {
            var deletedRecordId = await _inventoryLogic.DeleteRecord(recordId);
            return Ok(deletedRecordId);
        }
    }
}
