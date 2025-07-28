using System;

namespace MyInventory.Models
{
    public class Error
    {
        public Guid Id { get; set; }
        public int? Code { get; set; }
        public string? Details { get; set; }
        public string? StackTrace { get; set; }
    }
}
