using System.ComponentModel.DataAnnotations;

namespace APIADS.Models.Auth
{
    public class AuthenticateModel
    {
        [Required]
        public string NombreUsuario { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
