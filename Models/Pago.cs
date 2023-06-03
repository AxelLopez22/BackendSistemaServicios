using System;
using System.Collections.Generic;

namespace ApiServicios.Models
{
    public partial class Pago
    {
        public int Id { get; set; }
        public double? Total { get; set; }
        public DateTime? Fecha { get; set; }
        public int? IdClienteServicio { get; set; }
        public string? Mes { get; set; }

        public virtual ClienteServicio? IdClienteServicioNavigation { get; set; }
    }
}
