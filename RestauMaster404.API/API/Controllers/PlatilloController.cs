using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatilloController : ControllerBase, IPlatilloController
    {
        private readonly IPlatilloFlujo _platilloFlujo;
        private readonly ILogger<PlatilloController> _logger;

        public PlatilloController(IPlatilloFlujo platilloFlujo, ILogger<PlatilloController> logger)
        {
            _platilloFlujo = platilloFlujo;
            _logger = logger;
        }

        #region Operaciones

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _platilloFlujo.Obtener();
            if (!resultado.Any())
                return NoContent();
            return Ok(resultado);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Obtener([FromRoute] Guid Id)
        {
            var resultado = await _platilloFlujo.Obtener(Id);
            if (resultado == null)
                return NotFound("Platillo no encontrado.");
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] PlatilloRequest platillo)
        {
            var resultado = await _platilloFlujo.Agregar(platillo);
            return CreatedAtAction(nameof(Obtener), new { Id = resultado }, null);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Editar([FromRoute] Guid Id, [FromBody] PlatilloRequest platillo)
        {
            if (!await VerificarExiste(Id))
                return NotFound("Platillo no existe.");
            var resultado = await _platilloFlujo.Editar(Id, platillo);
            return Ok(resultado);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Eliminar([FromRoute] Guid Id)
        {
            if (!await VerificarExiste(Id))
                return NotFound("Platillo no existe.");
            await _platilloFlujo.Eliminar(Id);
            return NoContent();
        }

        #endregion Operaciones

        #region Helpers

        private async Task<bool> VerificarExiste(Guid Id)
        {
            var resultado = await _platilloFlujo.Obtener(Id);
            return resultado != null;
        }

        #endregion Helpers
    }
}