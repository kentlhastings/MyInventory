using System.Collections.Generic;

namespace MyInventory.Models
{
    public class Data
    {
        public int Version { get; set; }
        public List<Collection>? Collections { get; set; }
    }
}
