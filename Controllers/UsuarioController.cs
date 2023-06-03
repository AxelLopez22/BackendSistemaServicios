﻿using ApiServicios.Dto;
using ApiServicios.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiServicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServices _services;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(IUsuarioServices services, ILogger<UsuarioController> logger)
        {
            _services = services;
            _logger = logger;
        }

        [HttpPost("createUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateUser(UsuarioDTO model)
        {
            ModelRequest res = new ModelRequest();
            var result = await _services.AddUser(model);
            
            if(result == false)
            {
                _logger.LogError("Ha ocurrido un error al registrar usuario");
                res.status = "Error";
                res.data = "Ha ocurrio un error al registrar usuario";
                return BadRequest(res);
            }

            res.status = "Ok";
            res.data = "Usuario registrado con exito";

            return Ok(res);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            ModelRequest res = new ModelRequest();
            var result = await _services.Login(model);

            if(result == null)
            {
                _logger.LogError("Credenciales incorrectas");
                res.status = "Error";
                res.data = "Credenciales incorrectas";
                return BadRequest(res);
            }

            res.status = "Ok";
            res.data = result;

            return Ok(res);
        }
    }
}
