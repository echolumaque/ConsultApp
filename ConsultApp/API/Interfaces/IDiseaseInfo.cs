using System.Threading.Tasks;
using Refit;
using ConsultApp.API.Models;

namespace ConsultApp.API.Interfaces
{
    public interface IDiseaseInfo
    {
        [Get("/issues/{symptomID}/info?token={token}&language=en-gb")]
        Task<SymptomsDescription> GetSymptomsDescription(int symptomID, string token);
        //https://sandbox-healthservice.priaid.ch/issues/15/info?token=e&language=en-gb
    }
}
