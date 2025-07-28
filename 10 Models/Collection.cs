using System;
using System.Collections.Generic;

namespace MyInventory.Models
{
    public class Collection
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public List<Record>? Records { get; set; }
        public string? Details { get; set; }
        public double Value { get; set; }
    }
}
