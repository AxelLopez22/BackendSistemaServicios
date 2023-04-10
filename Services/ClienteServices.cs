using ApiServicios.Context;
using ApiServicios.Dto;
using ApiServicios.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiServicios.Services
{
    public class ClienteServices : IClienteServices
    {
        private readonly SistemaServiciosContext _context;

        public ClienteServices(SistemaServiciosContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateCliente(CreateClientDTO model)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                Cliente cliente = new Cliente();
                cliente.Nombres= model.Nombres;
                cliente.Apellidos= model.Apellidos;
                cliente.Correo= model.Correo;
                cliente.Celular= model.Celular;
                cliente.Inss = model.INSS;
                cliente.Cedula = model.Cedula;
                cliente.FechaNacimiento = model.FechaNacimiento;
                cliente.Direccion = model.direccion;
                cliente.Estado = true;

                await _context.Clientes.AddAsync(cliente);
                await _context.SaveChangesAsync();

                ClienteServicio servicio = new ClienteServicio();
                servicio.Estado = true;
                servicio.IdCliente = cliente.Id;
                servicio.Fecha = DateTime.Now;
                servicio.IdPlan = model.IdPlan;
                servicio.IdUsuario = 1;

                await _context.ClienteServicios.AddAsync(servicio);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> DeleteCliente(int id)
        {
            try
            {
                var cliente = await _context.Clientes.Where(x => x.Estado == true && x.Id == id).FirstOrDefaultAsync();

                if (cliente == null)
                {
                    return false;
                }

                cliente.Estado = false;

                _context.Clientes.Update(cliente);
                await _context.SaveChangesAsync();

                return true;
            } 
            catch
            {
                return false;
            }
        }

        public async Task<ClienteDTO> GetClienteId(int id)
        {
            try
            {
                var cliente = await _context.Clientes.Where(x => x.Estado == true && x.Id == id)
                    .Select(s => new ClienteDTO()
                    {
                        Id = s.Id,
                        Nombres = s.Nombres,
                        Apellidos = s.Apellidos,
                        Correo = s.Correo,
                        Celular = (int)s.Celular,
                        INSS = s.Inss,
                        Cedula = s.Cedula,
                        FechaNacimiento = (DateTime)s.FechaNacimiento,
                        direccion = s.Direccion
                    }).FirstOrDefaultAsync();

                if (cliente == null)
                {
                    return null;
                }

                return cliente;
            } catch 
            {
                return null;
            }
        }

        public async Task<List<ClienteDTO>> GetClientes()
        {
            var clientes = await _context.Clientes.Where(x => x.Estado == true)
                .Select(s => new ClienteDTO()
                {
                    Id = s.Id,
                    Nombres = s.Nombres,
                    Apellidos = s.Apellidos,
                    Correo = s.Correo,
                    Celular = (int)s.Celular,
                    INSS = s.Inss,
                    Cedula = s.Cedula,
                    FechaNacimiento = (DateTime)s.FechaNacimiento,
                    direccion = s.Direccion
                }).ToListAsync();

            if (clientes.Count() == 0)
            {
                return null;
            }

            return clientes;
        }

        public async Task<bool> UpdateCliente(int id, UpdateClienteDTO model)
        {
            try
            {
                var result = await _context.Clientes.Where(x => x.Id == id && x.Estado == true)
                    .FirstOrDefaultAsync();

                if (result == null)
                {
                    return false;
                }

                result.Nombres = model.Nombres;
                result.Apellidos = model.Apellidos;
                result.Correo = model.Correo;
                result.Celular = model.Celular;
                result.Direccion = model.direccion;

                _context.Clientes.Update(result);
                await _context.SaveChangesAsync();

                return true;
            } catch
            {
                return false;
            }
        }
    }

    public interface IClienteServices
    {
        Task<bool> CreateCliente(CreateClientDTO model);
        Task<bool> UpdateCliente(int id, UpdateClienteDTO model);
        Task<bool> DeleteCliente(int id);
        Task<List<ClienteDTO>> GetClientes();
        Task<ClienteDTO> GetClienteId(int id);
    }
}
