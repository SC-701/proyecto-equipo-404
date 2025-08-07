using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase, IVentaController
    {
        private readonly IVentaFlujo _ventaFlujo;
        private readonly ILogger<VentaController> _logger;

        public VentaController(IVentaFlujo ventaFlujo, ILogger<VentaController> logger)
        {
            _ventaFlujo = ventaFlujo;
            _logger = logger;
        }

        #region Operaciones

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _ventaFlujo.Obtener();
            if (!resultado.Any())
                return NoContent();
            return Ok(resultado);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Obtener([FromRoute] Guid Id)
        {
            var resultado = await _ventaFlujo.Obtener(Id);
            if (resultado == null)
                return NotFound("Venta no encontrada.");
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] VentaRequest venta)
        {
            var resultado = await _ventaFlujo.Agregar(venta);
            return CreatedAtAction(nameof(Obtener), new { Id = resultado }, null);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Editar([FromRoute] Guid Id, [FromBody] VentaRequest venta)
        {
            if (!await VerificarExiste(Id))
                return NotFound("La venta no existe.");
            var resultado = await _ventaFlujo.Editar(Id, venta);
            return Ok(resultado);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Eliminar([FromRoute] Guid Id)
        {
            if (!await VerificarExiste(Id))
                return NotFound("La venta no existe.");
            await _ventaFlujo.Eliminar(Id);
            return NoContent();
        }

        #endregion Operaciones

        #region Helpers

        private async Task<bool> VerificarExiste(Guid Id)
        {
            var resultado = await _ventaFlujo.Obtener(Id);
            return resultado != null;
        }

        #endregion Helpers
    }
}
