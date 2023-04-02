using System;
using System.Collections.Generic;

namespace ApiServicios.Models
{
    public partial class Plane
    {
        public Plane()
        {
            ClienteServicios = new HashSet<ClienteServicio>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public double Precio { get; set; }
        public bool? Estado { get; set; }
        public int? IdServicio { get; set; }

        public virtual Servicio? IdServicioNavigation { get; set; }
        public virtual ICollection<ClienteServicio> ClienteServicios { get; set; }
    }
}
