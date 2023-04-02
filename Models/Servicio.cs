using System;
using System.Collections.Generic;

namespace ApiServicios.Models
{
    public partial class Servicio
    {
        public Servicio()
        {
            Planes = new HashSet<Plane>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Plane> Planes { get; set; }
    }
}
