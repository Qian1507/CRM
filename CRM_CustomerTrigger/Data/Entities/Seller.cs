using Newtonsoft.Json;

namespace CRM_CustomerTrigger.Data.Entities
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
