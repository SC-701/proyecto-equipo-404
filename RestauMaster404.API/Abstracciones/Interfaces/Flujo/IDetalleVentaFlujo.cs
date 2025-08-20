using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IDetalleVentaFlujo
    {
        Task<IEnumerable<DetalleVentaResponse>> ObtenerPorVenta(Guid IdVenta);
        Task<DetalleVentaDetalle> Obtener(Guid Id);
        Task<Guid> Agregar(DetalleVentaRequest detalle);
        Task<Guid> Editar(Guid Id, DetalleVentaRequest detalle);
        Task<Guid> Eliminar(Guid Id);
    }
}
