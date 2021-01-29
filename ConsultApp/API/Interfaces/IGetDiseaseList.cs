using Refit;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ConsultApp.API.Models;

namespace ConsultApp.API.Interfaces
{
    public interface IGetDiseaseList
    {
        [Get("/issues?token={token}&language=en-gb")]
        Task<ObservableCollection<DiseaseInfoModel>> GetSymptoms(string token);
    }
}