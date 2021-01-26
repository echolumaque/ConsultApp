using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Prism.Commands;
using System.Threading;

namespace ConsultApp.API.Models
{
    public partial class DiagnosisModel
    {
        [JsonProperty("Issue")]
        public Issue Issue { get; set; }

        [JsonProperty("Specialisation")]
        public ObservableCollection<Specialisation> Specialisation { get; set; }
        public DelegateCommand<string> DoctorsPageCommand { get; set; }
    }

    public partial class Issue
    {
        [JsonProperty("ID")]
        public long Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Accuracy")]
        public double Accuracy { get; set; }

        [JsonProperty("IcdName")]
        public string IcdName { get; set; }

        [JsonProperty("Ranking")]
        public long Ranking { get; set; }
    }

    public partial class Specialisation
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
    }
}