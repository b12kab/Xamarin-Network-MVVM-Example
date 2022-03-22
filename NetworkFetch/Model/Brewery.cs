using System;
namespace NetworkFetch.Model
{
    public class Brewery
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public string id { get; set; }
        public string name { get; set; }
        public string brewery_type { get; set; }
        public string street { get; set; }
        public string address_2 { get; set; }       // likely null
        public string address_3 { get; set; }       // likely null
        public string city { get; set; }
        public string state { get; set; }
        public string county_province { get; set; } // likely null
        public string postal_code { get; set; }
        public string country { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string phone { get; set; }
        public string website_url { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime created_at { get; set; }
    }
}
