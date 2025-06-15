using System;
using System.Collections.Generic;

namespace MyInventory.Models
{
    public class Record
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public DateTime? Year { get; set; }
        public string? Notes { get; set; }
        public List<string>? Images { get; set; }
        public List<string>? Categories { get; set; }
        public DateTime PurchaseDate { get; set; }
        public double Price { get; set; }
    }
}
