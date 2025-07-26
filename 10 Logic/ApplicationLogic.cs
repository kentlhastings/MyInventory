using MyInventory.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyInventory.Logic
{
    public class ApplicationLogic
    {
        private static List<Collection> _collections = new List<Collection>();

        public async Task<Data> LoadData()
        {
            _collections = new List<Collection>()
            {
                new Collection
                {
                    Id = Guid.NewGuid(),
                    Name = "Power Tools",
                    Details = "Workshop cordless / corded tools",
                    Records = new List<Record>
                    {
                        new Record
                        {
                            Id = Guid.NewGuid(),
                            Make = "DeWalt",
                            Model = "DCD771C2",
                            Description = "18 V cordless drill",
                            Year = 2021,
                            Notes = "Used for house projects",
                            Images = new() { "C:\\Users\\Kent\\Documents\\Test Images\\sindell.jpg", "C:\\Users\\Kent\\Documents\\Test Images\\jade.jpg" },
                            Categories = new() { "Tools", "Power Tools" },
                            PurchaseDate = new DateTime(2021, 5, 15),
                            Price = 99.99,
                            Value = 80
                        },
                        new Record
                        {
                            Id = Guid.NewGuid(),
                            Make = "Honda",
                            Model = "HRX217VKA",
                            Description = "Self-propelled gas mower",
                            Year = 2020,
                            Notes = "Blades sharpened last summer",
                            Images = new() { "C:\\Users\\Kent\\Documents\\Test Images\\scorpion.jpg" },
                            Categories = new() { "Outdoor", "Tools" },
                            PurchaseDate = new DateTime(2020, 4, 10),
                            Price = 599,
                            Value = 450
                        }
                    }
                },
                new Collection
                {
                    Id = Guid.NewGuid(),
                    Name = "Furniture",
                    Details = "Indoor household furniture",
                    Records = new List<Record>
                    {
                        new Record
                        {
                            Id = Guid.NewGuid(),
                            Make = "IKEA",
                            Model = "LISABO",
                            Description = "Dining table w/ 6 chairs",
                            Year = 2019,
                            Notes = "One chair slightly worn",
                            Images = new() { "C:\\Users\\Kent\\Documents\\Test Images\\subzero.png" },
                            Categories = new() { "Furniture" },
                            PurchaseDate = new DateTime(2019, 11, 2),
                            Price = 450,
                            Value = 300
                        },
                        new Record
                        {
                            Id = Guid.NewGuid(),
                            Make = "Samsung",
                            Model = "UN55TU7000FXZA",
                            Description = "55\" 4K Smart TV",
                            Year = 2020,
                            Notes = "Mounted in living room",
                            Images = new(),
                            Categories = new() { "Electronics" },
                            PurchaseDate = new DateTime(2020, 9, 5),
                            Price = 399.99,
                            Value = 250
                        }
                    }
                }
            };
            return new Data()
            {
                Collections = _collections,
                Version = 1
            };
        }

        public async Task UpdateCollectionData(Collection collection)
        {
            var existingCollection = _collections.Find(c => c.Id == collection.Id);
            if (existingCollection != null) _collections[_collections.IndexOf(existingCollection)] = collection;
            else _collections.Add(collection);

            await SaveData();
        }

        public async Task UpdateCollectionData(Guid collectionId, Record record)
        {
            if (record.Id == Guid.Empty) record.Id = Guid.NewGuid();

            var collection = _collections.Find(c => c.Id == collectionId);
            if (collection is null) return;

            if (collection.Records is null) collection.Records = new List<Record>();

            var existingRecord = collection.Records.Find(r => r.Id == record.Id);
            if (existingRecord != null) collection.Records[collection.Records.IndexOf(existingRecord)] = record;
            else collection.Records.Add(record);

            await SaveData();
        }

        public async Task RemoveCollection(Guid collectionId)
        {
            var removedCount = _collections.RemoveAll(c => c.Id == collectionId);
            await SaveData();
        }

        public async Task RemoveRecord(Guid collectionId, Guid recordId)
        {
            var collection = _collections.Find(c => c.Id == collectionId);
            if (collection is null || collection.Records is null) return;

            var removedCount = collection.Records.RemoveAll(r => r.Id == recordId);

            await SaveData();
        }

        public async Task<bool> SaveData()
        {
            return true;
        }
    }
}
