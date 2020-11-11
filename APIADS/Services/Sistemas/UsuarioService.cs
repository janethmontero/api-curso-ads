using APIADS.Data.Sistemas;
using APIADS.Entities;
using APIADS.Models.Sistema;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIADS.Services.Sistemas
{
    public class UsuarioService : IUsuarioService
    {
        private readonly string _connectionString;

        private readonly UsuarioRepository _repository;
        public UsuarioService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("defaultConnection");
            _repository = new UsuarioRepository(_connectionString);
        }

        public async Task<List<Usuario>> GetUsuarios(int CveEmpresa)
        {
            return await _repository.GetUsuarios(CveEmpresa, null);
        }

        public async Task<Usuario> GetUsuario(int CveEmpresa, int cveUsuario)
        {
            var usuario = await _repository.GetUsuarios(CveEmpresa, cveUsuario);
            if (usuario == null)
            {
                return null;
            }
            return usuario.FirstOrDefault();
        }

        public async Task<ResponseModel> SaveUsuario(int CveEmpresa, Usuario usuario)
        {
            var CveUsuario = await _repository.InsertUsuario(CveEmpresa, usuario);
            ResponseModel response = new ResponseModel();
            response.IsSuccess = CveUsuario > 0;
            response.Result = CveUsuario;
            response.Message = CveUsuario > 0 ? "Usuario agregado correctamente." : "Ocurrio un error al agregar el usuario.";
            return response;
        }

        public async Task<ResponseModel> UpdateUsuario(int CveUsuario, Usuario usuario)
        {
            var CveUsuarioUpdated = await _repository.UpdatetUsuario(CveUsuario, usuario);
            ResponseModel response = new ResponseModel();
            response.IsSuccess = CveUsuarioUpdated > 0;
            response.Message = CveUsuarioUpdated > 0 ? "Usuario actualizado correctamente." : "Ocurrio un error al actualizar el usuario.";
            response.Result = CveUsuarioUpdated;
            return response;
        }

        public async Task<ResponseModel> DeleteUsuario(int CveUsuario)
        {
            var CveUsuarioDeleted = await _repository.DeleteUsuario(CveUsuario);
            ResponseModel response = new ResponseModel();
            response.IsSuccess = CveUsuarioDeleted > 0;
            response.Message = CveUsuarioDeleted > 0 ? "Usuario eliminado correctamente." : "Ocurrio un error al eliminar el usuario.";
            response.Result = CveUsuarioDeleted;
            return response;
        }
    }
}