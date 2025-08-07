using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

namespace Flujo
{
    public class VentaFlujo : IVentaFlujo
    {
        private readonly IVentaDA _ventaDA;

        public VentaFlujo(IVentaDA ventaDA)
        {
            _ventaDA = ventaDA;
        }

        #region Operaciones

        public async Task<Guid> Agregar(VentaRequest venta)
        {
            return await _ventaDA.Agregar(venta);
        }

        public async Task<Guid> Editar(Guid Id, VentaRequest venta)
        {
            await verificarVentaExiste(Id);
            return await _ventaDA.Editar(Id, venta);
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            await verificarVentaExiste(Id);
            return await _ventaDA.Eliminar(Id);
        }

        public async Task<IEnumerable<VentaResponse>> Obtener()
        {
            return await _ventaDA.Obtener();
        }

        public async Task<VentaResponse> Obtener(Guid Id)
        {
            return await _ventaDA.Obtener(Id);
        }

        #endregion

        #region Helpers

        private async Task verificarVentaExiste(Guid Id)
        {
            var resultado = await _ventaDA.Obtener(Id);
            if (resultado == null)
                throw new Exception("La venta no existe.");
        }

        #endregion
    }
}
