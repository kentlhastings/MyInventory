using System;
using System.Collections.Generic;

namespace MyInventory.Models
{
    public class Record
    {
        public Guid Id { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public string? Description { get; set; }
        public string? SerialNumber { get; set; }
        public int? Year { get; set; }
        public string? Notes { get; set; }
        public List<string>? Images { get; set; }
        public List<string>? Categories { get; set; }
        public DateTime PurchaseDate { get; set; }
        public double Price { get; set; }
        public double Value { get; set; }
    }
}
