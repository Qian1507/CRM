using Newtonsoft.Json;

namespace CRM.Api.Entities
{
    public class Customer
    {

        // JsonProperty maps the C# Id property to a lowercase "id" field in JSON
        // If no Id is provided when creating a new Customer, a new GUID string is generated
        [JsonProperty("id")]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = "";
        public string Title { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Email { get; set; } = "";
        public string Address { get; set; } = "";
        public Seller Seller { get; set; } = new Seller();
    }
}
