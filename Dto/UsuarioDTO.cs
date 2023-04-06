using System.ComponentModel.DataAnnotations;

namespace ApiServicios.Dto
{
    public class UsuarioDTO
    {
        [Required]
        public string NombreUsuario { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Correo { get; set; } = null!;
        [Required]
        public string? Contrasenia { get; set; }
    }

    public class LoginDTO
    {
        [Required]
        public string NombreUsuario { get; set; } = null!;
        [Required]
        public string Contrasenia { get; set; }
    }
}
