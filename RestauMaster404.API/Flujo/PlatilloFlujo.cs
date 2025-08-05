using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

namespace Flujo
{
    public class PlatilloFlujo : IPlatilloFlujo
    {
        private readonly IPlatilloDA _platilloDA;

        public PlatilloFlujo(IPlatilloDA platilloDA)
        {
            _platilloDA = platilloDA;
        }

        #region Operaciones

        public async Task<Guid> Agregar(PlatilloRequest platillo)
        {
            return await _platilloDA.Agregar(platillo);
        }

        public async Task<Guid> Editar(Guid Id, PlatilloRequest platillo)
        {
            await verificarPlatilloExiste(Id);
            return await _platilloDA.Editar(Id, platillo);
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            await verificarPlatilloExiste(Id);
            return await _platilloDA.Eliminar(Id);
        }

        public async Task<IEnumerable<PlatilloResponse>> Obtener()
        {
            return await _platilloDA.Obtener();
        }

        public async Task<PlatilloDetalle> Obtener(Guid Id)
        {
            return await _platilloDA.Obtener(Id);
        }

        #endregion

        #region Helpers

        private async Task verificarPlatilloExiste(Guid Id)
        {
            PlatilloDetalle resultado = await _platilloDA.Obtener(Id);
            if (resultado == null)
                throw new Exception("El platillo no existe.");
        }

        #endregion
    }
}