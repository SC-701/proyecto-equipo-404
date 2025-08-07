using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DA
{
    public class DetalleVentaDA : IDetalleVentaDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public DetalleVentaDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        #region Operaciones

        public async Task<Guid> Agregar(DetalleVentaRequest detalle)
        {
            string query = @"AgregarDetalleVenta";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Guid.NewGuid(),
                IdVenta = detalle.IdVenta,
                IdPlatillo = detalle.IdPlatillo,
                Cantidad = detalle.Cantidad,
                PrecioUnitario = detalle.PrecioUnitario,
                SubTotal = detalle.SubTotal
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Editar(Guid Id, DetalleVentaRequest detalle)
        {
            await verificarDetalleExiste(Id);
            string query = @"EditarDetalleVenta";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Id,
                IdVenta = detalle.IdVenta,
                IdPlatillo = detalle.IdPlatillo,
                Cantidad = detalle.Cantidad,
                PrecioUnitario = detalle.PrecioUnitario,
                SubTotal = detalle.SubTotal
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            await verificarDetalleExiste(Id);
            string query = @"EliminarDetalleVenta";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Id
            });
            return resultadoConsulta;
        }

        public async Task<IEnumerable<DetalleVentaResponse>> ObtenerPorVenta(Guid IdVenta)
        {
            string query = @"ObtenerDetallesVenta";
            var resultadoConsulta = await _sqlConnection.QueryAsync<DetalleVentaResponse>(query, new { IdVenta });
            return resultadoConsulta;
        }

        public async Task<DetalleVentaDetalle> Obtener(Guid Id)
        {
            string query = @"ObtenerDetalleVenta";
            var resultadoConsulta = await _sqlConnection.QueryAsync<DetalleVentaDetalle>(query, new
            {
                Id = Id
            });
            return resultadoConsulta.FirstOrDefault();
        }

        #endregion

        #region Helpers

        private async Task verificarDetalleExiste(Guid Id)
        {
            DetalleVentaDetalle? resultado = await Obtener(Id);
            if (resultado == null)
                throw new Exception("Detalle de venta no encontrado.");
        }

        #endregion
    }
}
