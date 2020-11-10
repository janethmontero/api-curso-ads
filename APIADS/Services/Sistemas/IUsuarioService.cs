using APIADS.Entities;
using APIADS.Models.Sistema;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIADS.Services.Sistemas
{
    public interface IUsuarioService
    {
        Task<List<Usuario>> GetUsuarios(int CveEmpresa);
        Task<Usuario> GetUsuario(int CveEmpresa, int cveUsuario);
        Task<ResponseModel> SaveUsuario(int CveEmpresa, Usuario usuario);
    }
}
