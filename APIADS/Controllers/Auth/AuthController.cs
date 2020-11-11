using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIADS.Entities;
using APIADS.Models.Auth;
using APIADS.Services.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIADS.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Authenticate")]
        public async Task<ActionResult<Usuario>> Authenticate([FromBody] AuthenticateModel model)
        {
            //if (model?.NombreUsuario == null && model?.Password == null)
            //    return null;

            var user = await _authService.Authenticate(model.NombreUsuario, model.Password);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}
