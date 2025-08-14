using Abstracciones.Modelos.Reportes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IReporteFlujo
    {
        Task<IEnumerable<ConteoPorTipoPlatillo>> ConteoPorTipoPlatillosAsync(bool soloDisponibles);
    }
}
