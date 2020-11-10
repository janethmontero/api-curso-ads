namespace APIADS.Models.Sistema
{
    public class UsuarioDto
    {
        public int CveUsuario { get; set; }
        public int CveEmpresa { get; set; }
        public string NombreUsuario { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Estatus { get; set; }
    }
}
