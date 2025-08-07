using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IPlatilloFlujo
    {
        Task<Guid> Agregar(PlatilloRequest platillo);
        Task<Guid> Editar(Guid Id, PlatilloRequest platillo);
        Task<Guid> Eliminar(Guid Id);
        Task<IEnumerable<PlatilloResponse>> Obtener();
        Task<PlatilloDetalle> Obtener(Guid Id);
    }
}
