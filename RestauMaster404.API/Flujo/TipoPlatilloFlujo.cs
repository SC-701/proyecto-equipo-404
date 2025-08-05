using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

namespace Flujo
{
    public class TipoPlatilloFlujo : ITipoPlatilloFlujo
    {
        private readonly ITipoPlatilloDA _tipoPlatilloDA;

        public TipoPlatilloFlujo(ITipoPlatilloDA tipoPlatilloDA)
        {
            _tipoPlatilloDA = tipoPlatilloDA;
        }

        public async Task<Guid> Agregar(TipoPlatilloRequest tipoPlatillo)
        {
            return await _tipoPlatilloDA.Agregar(tipoPlatillo);
        }

        public async Task<Guid> Editar(Guid Id, TipoPlatilloRequest tipoPlatillo)
        {
            return await _tipoPlatilloDA.Editar(Id, tipoPlatillo);
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            return await _tipoPlatilloDA.Eliminar(Id);
        }

        public async Task<IEnumerable<TipoPlatilloResponse>> Obtener()
        {
            return await _tipoPlatilloDA.Obtener();
        }

        public async Task<TipoPlatilloResponse> Obtener(Guid Id)
        {
            return await _tipoPlatilloDA.Obtener(Id);
        }
    }
}
