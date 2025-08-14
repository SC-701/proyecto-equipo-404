using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos.Reportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flujo
{
    public class ReporteFlujo : IReporteFlujo
    {
        private readonly IReporteDA _reporteDA;

        public ReporteFlujo(IReporteDA reporteDA)
        {
            _reporteDA = reporteDA;
        }

        #region Operaciones

        public async Task<IEnumerable<ConteoPorTipoPlatillo>> ConteoPorTipoPlatillosAsync(bool soloDisponibles)
        {
            return await _reporteDA.ConteoPorTipoPlatillosAsync(soloDisponibles);
        }

        #endregion
    }
}
