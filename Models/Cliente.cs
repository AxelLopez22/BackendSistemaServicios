using System;
using System.Collections.Generic;

namespace ApiServicios.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            ClienteServicios = new HashSet<ClienteServicio>();
        }

        public int Id { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string? Correo { get; set; }
        public int? Celular { get; set; }
        public string Inss { get; set; } = null!;
        public string Cedula { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public bool? Estado { get; set; }

        public virtual ICollection<ClienteServicio> ClienteServicios { get; set; }
    }
}
