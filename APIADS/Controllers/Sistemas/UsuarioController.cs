using System.Collections.Generic;
using System.Threading.Tasks;
using APIADS.Entities;
using APIADS.Models.Sistema;
using APIADS.Services.Sistemas;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIADS.Controllers.Sistemas
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioService _repository;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioService usuarioService, IMapper mapper)
        {
            _repository = usuarioService;
            _mapper = mapper;
        }

        // GET: api/Usuario
        [HttpGet]
        public async Task<ActionResult<UsuarioDto>> GetUsuarios()
        {
            //return await _repository.GetUsuarios(CveEmpresa);
            var usuarios = await _repository.GetUsuarios(GetCveEmpresa());
            var usuariosDTO = _mapper.Map<List<UsuarioDto>>(usuarios);
            if (usuariosDTO == null) { return NotFound(); }
            return Ok(usuariosDTO);
        }

        // GET: api/Usuario/16
        [HttpGet("{CveUsuario}")]
        public async Task<ActionResult<UsuarioDto>> GetUsuario(int CveUsuario) 
        {
            var usuario = await _repository.GetUsuario(GetCveEmpresa(), CveUsuario);
            var usuarioDTO = _mapper.Map<UsuarioDto>(usuario);
            if (usuarioDTO == null) { return NotFound(); }
            return Ok(usuarioDTO);
        }

        // POST api/Usuario
        [HttpPost]
        public async Task<ActionResult<ResponseModel>> PostUsuario([FromBody] UsuarioDto usuarioDto)
        {
            var usuario = _mapper.Map<Usuario>(usuarioDto);
            return await _repository.SaveUsuario(GetCveEmpresa(), usuario);
        }

        // PUT api/Usuario/16
        [HttpPut("{CveUsuario}")]
        public async Task<ActionResult<ResponseModel>> PutUsuario(int CveUsuario, [FromBody] UsuarioDto UsuarioDto)
        {
            if (UsuarioDto.CveUsuario != CveUsuario) return BadRequest();
            var usuario = _mapper.Map<Usuario>(UsuarioDto);
            return await _repository.UpdateUsuario(CveUsuario, usuario);
        }

        // DELETE api/Usuario/16
        [HttpDelete("{CveUsuario}")]
        public async Task<ActionResult<ResponseModel>> DeleteUsuario(int CveUsuario)
        {
            var usuario = await _repository.GetUsuario(GetCveEmpresa(), CveUsuario);
            if (usuario == null) return NotFound();
            var response = await _repository.DeleteUsuario(CveUsuario);
            if (response.IsSuccess) return response;
            else return BadRequest();
        }
    }
}