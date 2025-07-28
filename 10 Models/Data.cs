using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyInventory.Models
{
    public class Data
    {
        public int Version { get; set; }
        public List<Collection>? Collections { get; set; }
        public string? GridState { get; set; }

        public static string Serialize(Data data)
        {
            return JsonConvert.SerializeObject(data) ?? string.Empty;
        }

        public static Data Deserialize(string data)
        {
            return JsonConvert.DeserializeObject<Data>(data) ?? new Data();
        }
    }
}
