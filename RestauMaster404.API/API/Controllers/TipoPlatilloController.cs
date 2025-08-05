using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoPlatilloController : ControllerBase, ITipoPlatilloController
    {
        private readonly ITipoPlatilloFlujo _tipoPlatilloFlujo;
        private readonly ILogger<TipoPlatilloController> _logger;

        public TipoPlatilloController(ITipoPlatilloFlujo tipoPlatilloFlujo, ILogger<TipoPlatilloController> logger)
        {
            _tipoPlatilloFlujo = tipoPlatilloFlujo;
            _logger = logger;
        }

        #region Operaciones

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _tipoPlatilloFlujo.Obtener();
            if (!resultado.Any())
                return NoContent();
            return Ok(resultado);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Obtener([FromRoute] Guid Id)
        {
            var resultado = await _tipoPlatilloFlujo.Obtener(Id);
            if (resultado == null)
                return NotFound("Tipo de platillo no encontrado.");
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] TipoPlatilloRequest tipoPlatillo)
        {
            var resultado = await _tipoPlatilloFlujo.Agregar(tipoPlatillo);
            return CreatedAtAction(nameof(Obtener), new { Id = resultado }, null);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Editar([FromRoute] Guid Id, [FromBody] TipoPlatilloRequest tipoPlatillo)
        {
            if (!await VerificarExiste(Id))
                return NotFound("Tipo de platillo no existe.");
            var resultado = await _tipoPlatilloFlujo.Editar(Id, tipoPlatillo);
            return Ok(resultado);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Eliminar([FromRoute] Guid Id)
        {
            if (!await VerificarExiste(Id))
                return NotFound("Tipo de platillo no existe.");
            await _tipoPlatilloFlujo.Eliminar(Id);
            return NoContent();
        }

        #endregion Operaciones

        #region Helpers

        private async Task<bool> VerificarExiste(Guid Id)
        {
            var resultado = await _tipoPlatilloFlujo.Obtener(Id);
            return resultado != null;
        }

        #endregion Helpers
    }
}
