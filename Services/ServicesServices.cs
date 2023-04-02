using ApiServicios.Context;
using ApiServicios.Dto;
using Microsoft.EntityFrameworkCore;

namespace ApiServicios.Services
{
    public class ServicesServices : IServicesServices
    {
        private readonly SistemaServiciosContext _context;

        public ServicesServices(SistemaServiciosContext context)
        {
            _context = context;
        }

        public async Task<List<ServiciosDTO>> GetServicesAsync()
        {
            var result = await _context.Servicios.Where(x => x.Planes != null)
                .Select( s => new ServiciosDTO()
                {
                    Id= s.Id,
                    Nombre= s.Nombre,
                })
                .ToListAsync();

            return result;
        }
    }


    public interface IServicesServices
    {
        Task<List<ServiciosDTO>> GetServicesAsync();
    }
}
