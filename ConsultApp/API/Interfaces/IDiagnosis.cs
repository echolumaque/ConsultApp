using Refit;
using System.Threading.Tasks;
using ConsultApp.API.Models;
using System.Collections.ObjectModel;

namespace ConsultApp.API.Interfaces
{
    public interface IDiagnosis
    {
        [Get("/diagnosis?token={token}&language=en-gb&gender={gender}&year_of_birth={year}&symptoms={symptoms}")]
        Task<ObservableCollection<DiagnosisModel>> GetDisease(string token, string gender, int year, string symptoms);
    }
}
