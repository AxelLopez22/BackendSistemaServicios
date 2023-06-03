using ApiServicios.Dto;
using ApiServicios.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiServicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PagoController : ControllerBase
    {
        private readonly IPagoServices _services;
        private readonly ILogger<PagoController> _logger;

        public PagoController(ILogger<PagoController> logger, IPagoServices services)
        {
            _logger = logger;
            _services = services;
        }

        [HttpPost("agregarPago")]
        public async Task<IActionResult> RegistrarPago(PagoDTO model)
        {
            ModelRequest res = new ModelRequest();
            if(model == null)
            {
                _logger.LogError("Modelo no valido");
                res.status = "Error";
                res.data = "Modelo no valido";
                return BadRequest(res);
            }

            var result = await _services.AgregarPago(model);
            if(result == false)
            {
                _logger.LogError("Ocurrio un error al registrar pago");
                res.status = "Error";
                res.data = "Error al registrar el pago";
                return BadRequest(res);
            }

            res.status = "Ok";
            res.data = "Pago registrado con exito";
            return Ok(res);
        }

        [HttpGet("listarPagosClientes")]
        public async Task<IActionResult> VerPagosClientes()
        {
            ModelRequest res = new ModelRequest();
            var result = await _services.VerPagosClientes();

            if(result.Count() == 0)
            {
                res.status = "Error";
                res.data = "No hay pagos registrados";
                return BadRequest(res);
            }

            res.status = "Ok";
            res.data = result;

            return Ok(res);
        }

        [HttpGet("historialPagos/{id}")]
        public async Task<IActionResult> VerHistorialCliente(int id)
        {
            ModelRequest res = new ModelRequest();
            var result = await _services.HistorialPagos(id);
            if (result == null)
            {
                res.status = "Error";
                res.data = "Ocurrio un error al listar el historial de pagos";
                return BadRequest(res);
            }

            res.status = "Ok";
            res.data = result;

            return Ok(res);
        }
    }
}
