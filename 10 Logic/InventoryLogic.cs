using System;
using System.Threading.Tasks;
using MyInventory.Models;

namespace MyInventory.Logic
{
    public class InventoryLogic
    {
        private readonly ApplicationLogic _applicationLogic;

        public InventoryLogic(ApplicationLogic applicationLogic)
        {
            _applicationLogic = applicationLogic;
        }

        public async Task<Record> CreateRecord(Record record)
        {
            _applicationLogic.SaveData(null);
            return new Record();
        }

        public async Task<Record> UpdateRecord(Record record)
        {
            _applicationLogic.SaveData(null);
            return record;
        }

        public async Task<Guid> DeleteRecord(Guid recordId)
        {
            _applicationLogic.SaveData(null);
            return recordId;
        }
    }
}
