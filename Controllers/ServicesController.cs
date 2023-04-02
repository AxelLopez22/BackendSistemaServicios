﻿using ApiServicios.Dto;
using ApiServicios.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiServicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServicesServices _services;
        private readonly ILogger<ServicesController> _logger;

        public ServicesController(IServicesServices services, ILogger<ServicesController> logger) 
        {
            _services = services;
            _logger = logger;
        }

        [HttpGet("obtenerServicios")]
        public async Task<IActionResult> GetServices()
        {
            ModelRequest res = new ModelRequest();
            var result = await _services.GetServicesAsync();

            if (result == null)
            {
                _logger.LogError("Ocurrio un error al obtener servicios");
                res.status = "Error";
                res.data = "Error al cargar servicios";
                return BadRequest(res);
            }

            res.status = "Ok";
            res.data = result;

            return Ok(res);
        }
    }
}
