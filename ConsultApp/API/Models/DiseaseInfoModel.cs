using Newtonsoft.Json;

namespace ConsultApp.API.Models
{
    public class DiseaseInfoModel
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("ID")]
        public int ID { get; set; }
    }
}
