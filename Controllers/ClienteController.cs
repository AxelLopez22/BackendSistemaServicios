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
    public class ClienteController : ControllerBase
    {
        private readonly IClienteServices _services;
        private readonly ILogger<ClienteController> _logger;

        public ClienteController(IClienteServices services, ILogger<ClienteController> logger)
        {
            _services = services;
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IActionResult> ObtenerClientes()
        {
            ModelRequest res = new ModelRequest();
            var result = await _services.GetClientes();
            if (result == null)
            {
                _logger.LogError("Error al obtener Clientes");
                res.status = "Error";
                res.data = "Ocurrio un error al obtener clientes";
                return BadRequest(res);
            }

            res.status = "Ok";
            res.data = result;

            return Ok(res);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClienteId(int id)
        {
            ModelRequest res = new ModelRequest();
            var result = await _services.GetClienteId(id);
            if(result == null)
            {
                _logger.LogError("El cliente no existe");
                res.status = "Error";
                res.data = "El cliente no existe o ha sido eliminado";
                return NotFound(res);
            }

            res.status = "Ok";
            res.data = result;

            return Ok(res);
        }

        [HttpGet("ListarClientes")]
        public async Task<IActionResult> ListarClientes()
        {
            ModelRequest res = new ModelRequest();
            var result = await _services.ListarClientes();
            if(result.Count() == 0)
            {
                res.status = "Error";
                res.data = "La lista esta vacia";
                return BadRequest(res);
            }

            res.status = "Ok";
            res.data = result;

            return Ok(res);
        }

        [HttpGet("GetPlanPayment/{id}")]
        public async Task<IActionResult> GetPlanPayment(int id)
        {
            ModelRequest res = new ModelRequest();
            var result = await _services.GetPlanPayment(id);
            if(result == null)
            {
                res.status = "Error";
                res.data = "Error al listar el plan a pagar";
                return BadRequest(res);
            }

            res.status = "Ok";
            res.data = result;

            return Ok(res);
        }

        [HttpPost("CreateCliente")]
        public async Task<IActionResult> CreateCliente(CreateClientDTO model)
        {
            ModelRequest res = new ModelRequest();
            var result = await _services.CreateCliente(model);
            if(result == false)
            {
                _logger.LogError("Ocurrio un error al crear cliente");
                res.status = "Error";
                res.data = "Ocurrio un error al crear cliente";
                return BadRequest(res);
            }

            res.status = "Ok";
            res.data = "Cliente creado con exito";

            return Ok(res);
        }

        [HttpPut("actualizarCliente/{id}")]
        public async Task<IActionResult> UpdateCliente(int id, UpdateClienteDTO model)
        {
            ModelRequest res = new ModelRequest();

            var result = await _services.UpdateCliente(id, model);

            if(result == false)
            {
                _logger.LogError("Ocurrio un error al actualizar cliente");
                res.status = "Error";
                res.data = "Ocurrio un error al actualizar cliente";
                return BadRequest(res);
            }

            res.status = "Ok";
            res.data = "Cliente Actualizado con exito";

            return Ok(res);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            ModelRequest res = new ModelRequest();

            var result = await _services.DeleteCliente(id);
            if (result == false)
            {
                _logger.LogError("Error al eliminar cliente");
                res.status = "Error";
                res.data = "Ocurrio un error al eliminar cliente";
                return BadRequest(res);
            }

            res.status = "Error";
            res.data = "Cliente eliminado con exito";

            return Ok(res);
        }
    }
}
