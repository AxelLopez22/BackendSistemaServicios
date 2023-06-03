using System;
using System.Collections.Generic;

namespace ApiServicios.Models
{
    public partial class VwListarCliente
    {
        public int Id { get; set; }
        public string Cliente { get; set; } = null!;
    }
}
