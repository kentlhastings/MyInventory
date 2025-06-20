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

        public async Task<List<Collection>> GetTestCollections()
        {
            var powerTools = new Collection
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
                        Images = new() { "drill1.jpg", "drill2.jpg" },
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
                        Images = new() { "mower.jpg" },
                        Categories = new() { "Outdoor", "Tools" },
                        PurchaseDate = new DateTime(2020, 4, 10),
                        Price = 599,
                        Value = 450
                    }
                }
            };

            var furniture = new Collection
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
                        Images = new() { "table1.jpg" },
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
                        Images = new() { "tv.jpg" },
                        Categories = new() { "Electronics" },
                        PurchaseDate = new DateTime(2020, 9, 5),
                        Price = 399.99,
                        Value = 250
                    }
                }
            };

            foreach (var c in new[] { powerTools, furniture })
                c.Value = c.Records!.Sum(r => r.Value);

            return new List<Collection> { powerTools, furniture };
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
