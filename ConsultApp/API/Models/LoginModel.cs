using Newtonsoft.Json;

namespace ConsultApp.API.Models
{
    public class LoginModel
    {
        [JsonProperty("Token")]
        public string Token { get; set; }
    }
}
