using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.DA
{
    public interface ITipoPlatilloDA
    {
        Task<IEnumerable<TipoPlatilloResponse>> Obtener();
        Task<TipoPlatilloResponse> Obtener(Guid Id);
        Task<Guid> Agregar(TipoPlatilloRequest tipoPlatillo);
        Task<Guid> Editar(Guid Id, TipoPlatilloRequest tipoPlatillo);
        Task<Guid> Eliminar(Guid Id);
    }
}
