using System.Threading.Tasks;
using Refit;
using ConsultApp.API.Models;

namespace ConsultApp.API.Interfaces
{
    public interface IDiseaseInfo
    {
        [Get("/issues/{symptomID}/info?token={token}&language=en-gb")]
        Task<SymptomsDescription> GetSymptomsDescription(int symptomID, string token);
    }
}
