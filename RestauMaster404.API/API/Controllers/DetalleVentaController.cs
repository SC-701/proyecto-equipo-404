using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleVentaController : ControllerBase, IDetalleVentaController
    {
        private readonly IDetalleVentaFlujo _detalleVentaFlujo;
        private readonly ILogger<DetalleVentaController> _logger;

        public DetalleVentaController(IDetalleVentaFlujo detalleVentaFlujo, ILogger<DetalleVentaController> logger)
        {
            _detalleVentaFlujo = detalleVentaFlujo;
            _logger = logger;
        }

        #region Operaciones

        [HttpGet("obtenerTodos/{IdVenta}")]
        public async Task<IActionResult> ObtenerPorVenta([FromRoute] Guid IdVenta)
        {
            var resultado = await _detalleVentaFlujo.ObtenerPorVenta(IdVenta);
            if (!resultado.Any())
                return NoContent();
            return Ok(resultado);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Obtener([FromRoute] Guid Id)
        {
            var resultado = await _detalleVentaFlujo.Obtener(Id);
            if (resultado == null)
                return NotFound("Detalle de venta no encontrado.");
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] DetalleVentaRequest detalle)
        {
            var resultado = await _detalleVentaFlujo.Agregar(detalle);
            return CreatedAtAction(nameof(Obtener), new { Id = resultado }, null);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Editar([FromRoute] Guid Id, [FromBody] DetalleVentaRequest detalle)
        {
            if (!await VerificarExiste(Id))
                return NotFound("Detalle de venta no existe.");
            var resultado = await _detalleVentaFlujo.Editar(Id, detalle);
            return Ok(resultado);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Eliminar([FromRoute] Guid Id)
        {
            if (!await VerificarExiste(Id))
                return NotFound("Detalle de venta no existe.");
            await _detalleVentaFlujo.Eliminar(Id);
            return NoContent();
        }

        #endregion Operaciones

        #region Helpers

        private async Task<bool> VerificarExiste(Guid Id)
        {
            var resultado = await _detalleVentaFlujo.Obtener(Id);
            return resultado != null;
        }

        #endregion Helpers
    }
}
