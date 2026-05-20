using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Function.Data.Entities
{
    public class Seller
    {
        [JsonProperty("name")]
        public string Name { get; set; } = "";
        [JsonProperty("phone")]
        public string Phone { get; set; } = "";
        [JsonProperty("email")]
        public string Email { get; set; } = "";
    }
}
