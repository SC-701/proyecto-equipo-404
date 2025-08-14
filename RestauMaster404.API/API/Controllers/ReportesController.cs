using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteController : ControllerBase, IReporteController
    {
        private readonly IReporteFlujo _reporteFlujo;
        private readonly ILogger<ReporteController> _logger;

        public ReporteController(IReporteFlujo reporteFlujo, ILogger<ReporteController> logger)
        {
            _reporteFlujo = reporteFlujo;
            _logger = logger;
        }

        #region Operaciones

        // GET: /api/reporte/conteo-platillos-por-tipo?soloDisponibles=true
        [HttpGet("conteo-platillos-por-tipo")]
        [AllowAnonymous]
        public async Task<IActionResult> ConteoPlatillosPorTipo([FromQuery] bool soloDisponibles = false)
        {
            var resultado = await _reporteFlujo.ConteoPorTipoPlatillosAsync(soloDisponibles);
            if (!resultado.Any())
                return NoContent();
            return Ok(resultado);
        }

        #endregion Operaciones
    }
}
