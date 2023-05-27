using ApiServicios.Context;
using ApiServicios.Dto;
using ApiServicios.Models;

namespace ApiServicios.Services
{
    public class PagoServices : IPagoServices
    {
        private readonly SistemaServiciosContext _context;

        public PagoServices(SistemaServiciosContext context)
        {
            _context = context;
        }

        public async Task<bool> AgregarPago(PagoDTO model)
        {
            try
            {
                Pago pago = new Pago();
                pago.Total = model.Total;
                pago.Fecha = DateTime.Now;
                pago.IdClienteServicio = model.IdClienteServicio;
                
                await _context.Pagos.AddAsync(pago);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public interface IPagoServices
    {
        Task<bool> AgregarPago(PagoDTO model);
    }
}
