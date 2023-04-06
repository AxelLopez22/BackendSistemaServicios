using System.ComponentModel.DataAnnotations;

namespace ApiServicios.Dto
{
    public class ClienteServicioDTO
    {
        [Required]
        public int? IdCliente { get; set; }
        [Required]
        public int? IdPlan { get; set; }
        [Required]
        public int? IdUsuario { get; set; }
    }
}
