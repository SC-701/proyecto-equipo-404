using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadoController : ControllerBase, IEstadoController
    {
        private readonly IEstadoFlujo _estadoFlujo;
        private readonly ILogger<EstadoController> _logger;

        public EstadoController(IEstadoFlujo estadoFlujo, ILogger<EstadoController> logger)
        {
            _estadoFlujo = estadoFlujo;
            _logger = logger;
        }

        #region Operaciones

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _estadoFlujo.Obtener();
            if (!resultado.Any())
                return NoContent();
            return Ok(resultado);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Obtener([FromRoute] Guid Id)
        {
            var estado = await _estadoFlujo.Obtener(Id);
            if (estado == null)
                return NotFound("Estado no encontrado.");
            return Ok(estado);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] EstadoRequest estado)
        {
            var id = await _estadoFlujo.Agregar(estado);
            return CreatedAtAction(nameof(Obtener), new { Id = id }, null);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Editar([FromRoute] Guid Id, [FromBody] EstadoRequest estado)
        {
            if (!await VerificarEstadoExiste(Id))
                return NotFound("Estado no encontrado.");

            var resultado = await _estadoFlujo.Editar(Id, estado);
            return Ok(resultado);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Eliminar([FromRoute] Guid Id)
        {
            if (!await VerificarEstadoExiste(Id))
                return NotFound("Estado no encontrado.");

            await _estadoFlujo.Eliminar(Id);
            return NoContent();
        }

        #endregion Operaciones

        #region Helpers

        private async Task<bool> VerificarEstadoExiste(Guid Id)
        {
            var resultadoEstado = await _estadoFlujo.Obtener(Id);
            return resultadoEstado != null;
        }

        #endregion Helpers
    }
}