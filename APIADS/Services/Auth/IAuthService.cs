using APIADS.Entities;
using System.Threading.Tasks;

namespace APIADS.Services.Auth
{
    public interface IAuthService
    {
        Task<Usuario> Authenticate(string nombreUsuario, string password);
    }
}
