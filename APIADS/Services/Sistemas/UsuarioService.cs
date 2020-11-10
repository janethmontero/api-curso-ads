using APIADS.Data.Sistemas;
using APIADS.Entities;
using APIADS.Models.Sistema;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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

        public Task<Usuario> GetUsuario(int CveEmpresa, int cveUsuario)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel> SaveUsuario(int CveEmpresa, Usuario usuario)
        {
            var CveUsuario = await _repository.InsertUsuario(CveEmpresa, usuario);
            ResponseModel response = new ResponseModel();
            response.IsSuccess = CveUsuario > 0;
            response.Message = CveUsuario > 0 ? "Usuario agregado correctamente." : "Ocurrio un error al agregar el usuario.";
            return response;
        }
    }
}