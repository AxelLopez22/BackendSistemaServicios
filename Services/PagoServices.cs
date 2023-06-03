using ApiServicios.Dto;
using ApiServicios.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiServicios.Services
{
    public class PagoServices : IPagoServices
    {
        private readonly sistemaserviciosContext _context;

        public PagoServices(sistemaserviciosContext context)
        {
            _context = context;
        }

        public async Task<bool> AgregarPago(PagoDTO model)
        {
            try
            {
                Pago pago = new Pago();
                pago.Total = model.Total;
                pago.Mes = model.Mes;
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

        public async Task<HistorialPagosClientesDTO> HistorialPagos(int IdCliente)
        {
            try
            {
                var result = await (from clienteServicio in _context.ClienteServicios
                                    where clienteServicio.IdCliente == IdCliente // Filtras por el estado del servicio si es necesario
                                    select new HistorialPagosClientesDTO()
                                    {
                                        Cliente = clienteServicio.IdClienteNavigation.Nombres + " " + clienteServicio.IdClienteNavigation.Apellidos,
                                        Plan = clienteServicio.IdPlanNavigation.Nombre,
                                        Pagos = clienteServicio.Pagos.Select(p => new MesesPagosDTO()
                                        {
                                            FechaPago = (DateTime)p.Fecha,
                                            Mes = p.Mes
                                        }).ToList()
                                    }).FirstOrDefaultAsync();

                return result;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<PagosClientesSP>> VerPagosClientes()
        {
            var result = await _context.PagosClientesSP.FromSqlRaw("EXEC sp_ListarPagosClientes").ToListAsync();
            return result;
        }
    }

    public interface IPagoServices
    {
        Task<bool> AgregarPago(PagoDTO model);
        Task<List<PagosClientesSP>> VerPagosClientes();
        Task<HistorialPagosClientesDTO> HistorialPagos(int IdCliente);
    }
}
