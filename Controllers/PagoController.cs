using ApiServicios.Dto;
using ApiServicios.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiServicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}
