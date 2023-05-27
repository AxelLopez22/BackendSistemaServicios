namespace ApiServicios.Dto
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Celular { get; set; }
        public string Servicio { get; set; }
        public string direccion { get; set; }
    }

    public class ClienteDTOById
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Celular { get; set; }
        public string Correo { get; set; }
        public string Servicio { get; set; }
        public string direccion { get; set; }
    }

    public class CreateClientDTO
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public int Celular { get; set; }
        public string INSS { get; set; }
        public string Cedula { get; set; }
        public string direccion { get; set; }
        public int IdPlan { get; set; }
    }

    public class UpdateClienteDTO
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public int Celular { get; set; }
        public string direccion { get; set; }
        public int IdServicio { get; set; }
    }
}
