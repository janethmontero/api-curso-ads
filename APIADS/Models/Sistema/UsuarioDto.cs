using System.ComponentModel.DataAnnotations;

namespace APIADS.Models.Sistema
{
    public class UsuarioDto
    {
        public int CveUsuario { get; set; }
        public int CveEmpresa { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "El campo Nombre Usuario debe tener {1} caracteres o menos.")]
        public string NombreUsuario { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Estatus { get; set; }
    }
}
