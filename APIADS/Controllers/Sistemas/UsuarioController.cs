using System.Collections.Generic;
using System.Threading.Tasks;
using APIADS.Entities;
using APIADS.Models.Sistema;
using APIADS.Services.Sistemas;
using Microsoft.AspNetCore.Mvc;

namespace APIADS.Controllers.Sistemas
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioService _repository;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _repository = usuarioService;
        }

        // GET: api/Usuario
        [HttpGet("{CveEmpresa}")]
        public async Task<List<Usuario>> Get(int CveEmpresa)
        {
            return await _repository.GetUsuarios(CveEmpresa);
        }

        // POST api/Usuario
        [HttpPost("{CveEmpresa}")]
        public async Task<ActionResult<ResponseModel>> Post(int CveEmpresa, [FromBody] Usuario usuario)
        {
            return await _repository.SaveUsuario(CveEmpresa, usuario);
        }
    }
}
