using ApiServicios.Context;
using ApiServicios.Dto;
using ApiServicios.Models;

namespace ApiServicios.Services
{
    public class ClienteServicioServices : IClienteServicio
    {
        private readonly SistemaServiciosContext _context;

        public ClienteServicioServices(SistemaServiciosContext context)
        {
            _context = context;
        }

        public async Task<bool> asingarServicio(ClienteServicioDTO model)
        {
            try
            {
                ClienteServicio addServicio = new ClienteServicio();
                addServicio.IdCliente = model.IdCliente;
                addServicio.IdUsuario = model.IdUsuario;
                addServicio.IdPlan = model.IdPlan;
                addServicio.Fecha = DateTime.Now;
                addServicio.Estado = true;

                await _context.ClienteServicios.AddAsync(addServicio);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public interface IClienteServicio
    {
        Task<bool> asingarServicio(ClienteServicioDTO model);
    }
}
