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

        [HttpGet("listarServicio")]
        public async Task<IActionResult> ListarServicio()
        {
            ModelRequest res = new ModelRequest();
            var result = await _services.ListServices();

            if(result.Count == 0)
            {
                _logger.LogError("Error al listar servicios");
                res.status = "Error";
                res.data = "Ocurrio un error al listar el servicio";
                return BadRequest(res);
            }

            res.status = "Ok";
            res.data = result;

            return Ok(res);
        }

        [HttpGet("categoriaServicios")]
        public async Task<IActionResult> GetCategories()
        {
            ModelRequest res = new ModelRequest();
            var result = await _services.VerCategorias();

            if(result.Count == 0)
            {
                res.status = "Error";
                res.data = "La lista esta vacia";
            }

            res.status = "Ok";
            res.data = result;
            return Ok(res);
        }

        [HttpPost("crearPlan")]
        public async Task<IActionResult> CrearPlan(CrearPlanDTO model)
        {
            ModelRequest res = new ModelRequest();
            var result = await _services.CrearPlan(model);

            if(result == false)
            {
                res.status = "Error";
                res.data = "Ocurrio un error al crear plan";
                return BadRequest(res);
            }

            res.status = "Ok";
            res.data = "Plan creado con exito";

            return Ok(res);
        }
    }
}
