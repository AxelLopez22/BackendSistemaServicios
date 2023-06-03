using ApiServicios.Dto;
using ApiServicios.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiServicios.Services
{
    public class ServicesServices : IServicesServices
    {
        private readonly sistemaserviciosContext _context;

        public ServicesServices(sistemaserviciosContext context)
        {
            _context = context;
        }

        public async Task<bool> CrearPlan(CrearPlanDTO model)
        {
            try
            {
                Plane plan = new Plane();
                plan.Nombre = model.Nombre;
                plan.Descripcion = model.Descripcion;
                plan.Precio = model.Precio;
                plan.IdServicio = model.IdServicio;
                plan.Estado = true;

                await _context.Planes.AddAsync(plan);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> EditarPlanDTO(int IdPlan, EditarPlanDTO model)
        {
            try
            {
                var Plan = await _context.Planes.Where(x => x.Id == IdPlan).FirstOrDefaultAsync();

                if(Plan != null)
                {
                    Plan.Nombre = model.Nombre;
                    Plan.Descripcion = model.Descripcion;
                    Plan.Precio = model.Precio;

                    _context.Planes.Update(Plan);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch 
            {
                return false;
            }   
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
                    Id = s.Id,
                    Categoria = s.IdServicioNavigation.Nombre,
                    Nombre = s.Nombre,
                    Descripcion = s.Descripcion,
                    Precio = s.Precio,
                }).OrderBy(x => x.Categoria).ToListAsync();

            return result;
        }

        public async Task<List<CategoriaServiciosDTO>> VerCategorias()
        {
            var result = await _context.Servicios
                .Select(x => new CategoriaServiciosDTO()
                {
                    Id = x.Id,
                    Nombre= x.Nombre,
                })
                .ToListAsync();

            return result;
        }

        public async Task<EditarPlanDTO> VerServicioId(int Id)
        {
            var servicio = await _context.Planes.Where(x => x.Id == Id && x.Estado == true)
                .Select(x => new EditarPlanDTO() 
                {
                    Nombre = x.Nombre,
                    Descripcion = x.Descripcion,
                    Precio = x.Precio
                })
                .FirstOrDefaultAsync();

            return servicio;
        }
    }


    public interface IServicesServices
    {
        Task<List<ServiciosDTO>> GetServicesAsync();
        Task<List<VerServiciosDTO>> ListServices();
        Task<List<CategoriaServiciosDTO>> VerCategorias();
        Task<bool> CrearPlan(CrearPlanDTO model);
        Task<bool> EditarPlanDTO(int IdPlan, EditarPlanDTO model);
        Task<EditarPlanDTO> VerServicioId(int Id);
    }
}
