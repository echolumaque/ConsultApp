using Newtonsoft.Json;

namespace ConsultApp.API.Models
{
    public class SymptomsDescription
    {
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("MedicalCondition")]
        public string MedicalCondition { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("ProfName")]
        public string ProfName { get; set; }
        [JsonProperty("PossibleSymptoms")]
        public string PossibleSymptoms { get; set; }
        [JsonProperty("TreatmentDescription")]
        public string TreatmentDescription { get; set; }
        //https://sandbox-healthservice.priaid.ch/issues/15/info?token=e&language=en-gb
    }
}
