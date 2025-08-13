using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Flujo
{
    public interface ITipoPlatilloFlujo
    {
        Task<IEnumerable<TipoPlatilloResponse>> Obtener();
        Task<TipoPlatilloResponse> Obtener(Guid Id);
        Task<Guid> Agregar(TipoPlatilloRequest tipoPlatillo);
        Task<Guid> Editar(Guid Id, TipoPlatilloRequest tipoPlatillo);
        Task<Guid> Eliminar(Guid Id);
        Task<int> Contar();
    }
}