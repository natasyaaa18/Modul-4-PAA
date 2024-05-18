using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task4_paa.models
{
    internal class Product
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("price")]
        public int price { get; set; }
        [JsonProperty("stock")]
        public int stock { get; set; }
    }
}
