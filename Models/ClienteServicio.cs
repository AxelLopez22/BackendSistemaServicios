using System;
using System.Collections.Generic;

namespace ApiServicios.Models
{
    public partial class ClienteServicio
    {
        public ClienteServicio()
        {
            Pagos = new HashSet<Pago>();
        }

        public int Id { get; set; }
        public DateTime? Fecha { get; set; }
        public bool? Estado { get; set; }
        public int? IdCliente { get; set; }
        public int? IdPlan { get; set; }
        public int? IdUsuario { get; set; }

        public virtual Cliente? IdClienteNavigation { get; set; }
        public virtual Plane? IdPlanNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual ICollection<Pago> Pagos { get; set; }
    }
}
