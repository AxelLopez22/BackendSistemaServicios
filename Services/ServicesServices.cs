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
                    servicios = s.Planes.Select(x => new ServicesDetailsDTO()
                    {
                        Id= x.Id,
                        Nombre = x.Nombre,
                        Descripcion = x.Descripcion,
                        Precio = x.Precio,
                        IdServicio = x.IdServicio,
                    }).ToList(),
                })
                .ToListAsync();

            return result;
        }

        public async Task<List<VerServiciosDTO>> ListServices()
        {
            var result = await _context.Planes.Where(x => x.Estado == true)
                .Select(s => new VerServiciosDTO()
                {
                    Categoria = s.IdServicioNavigation.Nombre,
                    Nombre = s.Nombre,
                    Descripcion = s.Descripcion,
                    Precio = s.Precio,
                }).OrderBy(x => x.Categoria).ToListAsync();

            return result;
        }
    }


    public interface IServicesServices
    {
        Task<List<ServiciosDTO>> GetServicesAsync();
        Task<List<VerServiciosDTO>> ListServices();
    }
}
