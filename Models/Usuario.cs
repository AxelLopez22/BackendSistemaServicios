using System;
using System.Collections.Generic;

namespace ApiServicios.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            ClienteServicios = new HashSet<ClienteServicio>();
        }

        public int Id { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string? Contrasenia { get; set; }

        public virtual ICollection<ClienteServicio> ClienteServicios { get; set; }
    }
}
