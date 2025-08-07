using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.DA
{
    public interface IVentaDA
    {
        Task<IEnumerable<VentaResponse>> Obtener();
        Task<VentaResponse> Obtener(Guid Id);
        Task<Guid> Agregar(VentaRequest venta);
        Task<Guid> Editar(Guid Id, VentaRequest venta);
        Task<Guid> Eliminar(Guid Id);
    }
}