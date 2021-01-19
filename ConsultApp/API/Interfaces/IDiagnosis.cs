using ConsultApp.API.Models;
using Refit;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ConsultApp.API.Interfaces
{
    public interface IDiagnosis
    {
        [Get("/diagnosis?token={token}&language=en-gb&gender=male&year_of_birth=1999&symptoms={symptoms}")]
        Task<ObservableCollection<DiagnosisModel>> GetDisease(string token, string symptoms);
        //$"{APIConfig.HealthApi}/diagnosis?token={APIConfig.Token}&language=en-gb&gender=male&year_of_birth=1999&symptoms={symptoms}"
    }
}
