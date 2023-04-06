using ApiServicios.Dto;
using ApiServicios.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiServicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteServicioController : ControllerBase
    {
        private readonly IClienteServicio _services;
        private ILogger<ClienteServicioController> _logger;

        public ClienteServicioController(IClienteServicio services, ILogger<ClienteServicioController> logger)
        {
            _services = services;
            _logger = logger;
        }

        [HttpPost("asignarServicio")]
        public async Task<IActionResult> AddServicioCliente(ClienteServicioDTO model)
        {
            ModelRequest res = new ModelRequest();
            var result = await _services.asingarServicio(model);

            if(result == false)
            {
                _logger.LogError("Error al asginar servicio");
                res.status = "Error";
                res.data = "Ocurrio un error al asignar resgistro";
                return BadRequest(res);
            }

            res.status = "Ok";
            res.data = "Servicio asignado con exito";

            return Ok(res);
        }
    }
}
