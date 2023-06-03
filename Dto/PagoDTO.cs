using System.ComponentModel.DataAnnotations;

namespace ApiServicios.Dto
{
    public class PagoDTO
    {
        public double? Total { get; set; }
        public string Mes { get; set; }
        public int? IdClienteServicio { get; set; }
    }

    public class PagosClientesSP
    {
        [Key]
        public int Id { get; set; }
        public string Cliente { get; set; }
        public double Monto { get; set; }
        public string? Mes { get; set; } = null;
        public int IdServicio { get; set; }
        public string Servicio { get; set; }
    }

    public class HistorialPagosClientesDTO
    {
        public string Cliente { get; set; }
        public string Plan { get; set; }
        public List<MesesPagosDTO> Pagos { get; set; }

        public HistorialPagosClientesDTO()
        {
            this.Pagos = new List<MesesPagosDTO>();
        }
    }

    public class MesesPagosDTO
    {
        public string Mes { get; set; }
        public DateTime FechaPago { get; set;}
    }
}
