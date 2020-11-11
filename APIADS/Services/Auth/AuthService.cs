using APIADS.Data.Auth;
using APIADS.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace APIADS.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly AuthRepository _repository;
        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("defaultConnection");
            _repository = new AuthRepository(_connectionString);
        }

        public async Task<Usuario> Authenticate(string nombreUsuario, string password)
        {
            Usuario user = await _repository.Login(nombreUsuario, password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("CveEmpresa", user.CveEmpresa.ToString()),
                    new Claim("CveUsuario", user.CveUsuario.ToString()),
                    new Claim("CveRol", "ADS-1")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            //asignamos token
            user.AccessToken = tokenHandler.WriteToken(token);

            return user; // TODO: mappear a un dto para quitar password
        }
    }
}
