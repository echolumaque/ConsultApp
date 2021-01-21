using Refit;
using ConsultApp.API.Models;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace ConsultApp.API.Interfaces
{
    public interface IGetDiseaseList
    {
        [Get("/issues?token={token}&language=en-gb")]
        Task<ObservableCollection<DiseaseInfoModel>> GetSymptoms(string token);
    }
}