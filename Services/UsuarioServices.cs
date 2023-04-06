using ApiServicios.Common;
using ApiServicios.Context;
using ApiServicios.Dto;
using ApiServicios.Models;

namespace ApiServicios.Services
{
    public class UsuarioServices : IUsuarioServices
    {
        private readonly SistemaServiciosContext _context;

        public UsuarioServices(SistemaServiciosContext context)
        {
            _context = context;
        }

        public async Task<bool> AddUser(UsuarioDTO model)
        {
            try
            {
                string contraseniaEncrypt = Encrypt.GetSHA256(model.Contrasenia);

                Usuario user = new Usuario();
                user.NombreUsuario = model.NombreUsuario;
                user.Correo = model.Correo;
                user.Contrasenia = contraseniaEncrypt;

                await _context.Usuarios.AddAsync(user);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public Task<bool> Login(LoginDTO model)
        {
            throw new NotImplementedException();
        }
    }

    public interface IUsuarioServices
    {
        Task<bool> AddUser(UsuarioDTO model);
        Task<bool> Login(LoginDTO model);
    }
}
