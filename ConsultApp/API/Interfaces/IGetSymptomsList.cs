using Refit;
using ConsultApp.API.Models;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace ConsultApp.API.Interfaces
{
    public interface IGetSymptomsList
    {
        [Get("/symptoms?token={token}&language=en-gb")]
        Task<ObservableCollection<SymptomsModel>> GetSymptoms(string token);
    }
}
