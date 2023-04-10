using ApiServicios.Common;
using ApiServicios.Context;
using ApiServicios.Dto;
using ApiServicios.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiServicios.Services
{
    public class UsuarioServices : IUsuarioServices
    {
        private readonly SistemaServiciosContext _context;
        private readonly IConfiguration _config;

        public UsuarioServices(SistemaServiciosContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
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

        public async Task<AuthResponse> Login(LoginDTO model)
        {
            var user = await _context.Usuarios.Where(x => x.NombreUsuario == model.NombreUsuario).FirstOrDefaultAsync();
            var contraseniaEncrypt = Encrypt.GetSHA256(model.Contrasenia);

            if (user != null)
            {
                if(user.NombreUsuario == model.NombreUsuario && user.Contrasenia == contraseniaEncrypt)
                {
                    return await ConstruirToken(user);
                } else
                {
                    return null;
                }
            }

            return null;
        }

        //Generar token
        private async Task<AuthResponse> ConstruirToken(Usuario user)
        {
            var Claims = new List<Claim>()
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("UserName", user.NombreUsuario),
                new Claim("Correo", user.Correo)
            };

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["LlaveJwt"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var Expiracion = DateTime.UtcNow.AddHours(1);
            var securityToken = new JwtSecurityToken(issuer: "localhost", audience: "localhost", claims: Claims,
                expires: Expiracion, signingCredentials: creds);

            return new AuthResponse()
            {
                token = new JwtSecurityTokenHandler().WriteToken(securityToken)
            };
        }
    }

    public interface IUsuarioServices
    {
        Task<bool> AddUser(UsuarioDTO model);
        Task<AuthResponse> Login(LoginDTO model);
    }
}
