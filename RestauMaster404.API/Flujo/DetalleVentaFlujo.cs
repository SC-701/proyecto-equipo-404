using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;

namespace Flujo
{
    public class DetalleVentaFlujo : IDetalleVentaFlujo
    {
        private readonly IDetalleVentaDA _detalleVentaDA;

        public DetalleVentaFlujo(IDetalleVentaDA detalleVentaDA)
        {
            _detalleVentaDA = detalleVentaDA;
        }

        #region Operaciones

        public async Task<Guid> Agregar(DetalleVentaRequest detalle)
        {
            return await _detalleVentaDA.Agregar(detalle);
        }

        public async Task<Guid> Editar(Guid Id, DetalleVentaRequest detalle)
        {
            await verificarDetalleExiste(Id);
            return await _detalleVentaDA.Editar(Id, detalle);
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            await verificarDetalleExiste(Id);
            return await _detalleVentaDA.Eliminar(Id);
        }

        public async Task<IEnumerable<DetalleVentaResponse>> ObtenerPorVenta(Guid IdVenta)
        {
            return await _detalleVentaDA.ObtenerPorVenta(IdVenta);
        }


        public async Task<DetalleVentaDetalle> Obtener(Guid Id)
        {
            return await _detalleVentaDA.Obtener(Id);
        }

        #endregion

        #region Helpers

        private async Task verificarDetalleExiste(Guid Id)
        {
            DetalleVentaDetalle? detalle = await _detalleVentaDA.Obtener(Id);
            if (detalle == null)
                throw new Exception("El detalle de venta no existe.");
        }

        #endregion
    }
}
