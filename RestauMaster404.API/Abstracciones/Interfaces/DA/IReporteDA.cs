using Abstracciones.Modelos.Reportes;

namespace Abstracciones.Interfaces.DA
{
    public interface IReporteDA
    {
        Task<IEnumerable<ConteoPorTipoPlatillo>> ConteoPorTipoPlatillosAsync(bool soloDisponibles);
    }
}
