using Refit;
using System.Threading.Tasks;
using ConsultApp.API.Models;

namespace ConsultApp.API.Intefaces
{
    public partial interface IAuthApi
    {
        [Post("/login")]
        [Headers("Authorization: Bearer")]
        Task<LoginModel> GetToken();
    }
}
