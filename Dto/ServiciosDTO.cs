namespace ApiServicios.Dto
{
    public class ServiciosDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<ServicesDetailsDTO> servicios { get; set; }

        public ServiciosDTO()
        {
            this.servicios = new List<ServicesDetailsDTO>();
        }
    }

    public class ServicesDetailsDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public double Precio { get; set; }
        public int? IdServicio { get; set; }
    }

    public class VerServiciosDTO
    {
        public string Categoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
    }
}
