using MyInventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyInventory.Logic
{
    public class InventoryLogic
    {
        private readonly ApplicationLogic _applicationLogic;

        public InventoryLogic(ApplicationLogic applicationLogic)
        {
            _applicationLogic = applicationLogic;
        }

        public async Task<List<Collection>> GetCollections()
        {
            var collectionData = await _applicationLogic.LoadData();
            collectionData?.Collections?.ForEach(c => SetCollectionValue(c));
            return collectionData?.Collections ?? new List<Collection>();
        }

        public Collection? GetCollectionValue(Guid collectionId)
        {
            var collection = _applicationLogic.GetCollection(collectionId);
            if (collection != null) SetCollectionValue(collection);
            return collection;
        }

        public void SetCollectionValue(Collection collection)
        {
            if (collection != null)
            {
                if (collection.Records != null) collection.Value = collection.Records.Sum(r => r.Value);
                else collection.Value = 0;
            }
        }

        public async Task<Record> CreateRecord(Guid collectionId, Record record)
        {
            await _applicationLogic.UpdateCollectionData(collectionId, record);
            return record;
        }

        public async Task<Record> UpdateRecord(Guid collectionId, Record record)
        {
            await _applicationLogic.UpdateCollectionData(collectionId, record);
            return record;
        }

        public async Task<Guid> DeleteRecord(Guid collectionId, Guid recordId)
        {
            await _applicationLogic.RemoveRecord(collectionId, recordId);
            return recordId;
        }

        public async Task<Guid> CreateCollection(Collection collection)
        {
            await _applicationLogic.UpdateCollectionData(collection);
            return collection.Id;
        }

        public async Task<Collection> UpdateCollection(Collection collection)
        {
            await _applicationLogic.UpdateCollectionData(collection);
            return collection;
        }

        public async Task<Guid> DeleteCollection(Guid collectionId)
        {
            await _applicationLogic.RemoveCollection(collectionId);
            return collectionId;
        }

        public async Task<Error> ReportError(Error error)
        {
            return error;
        }
    }
}
