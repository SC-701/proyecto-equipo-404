using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DA
{
    public class VentaDA : IVentaDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public VentaDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        #region Operaciones

        public async Task<Guid> Agregar(VentaRequest venta)
        {
            string query = @"AgregarVenta";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Guid.NewGuid(),
                Fecha = venta.Fecha,
                Hora = venta.Hora,
                Total = venta.Total
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Editar(Guid Id, VentaRequest venta)
        {
            await verificarVentaExiste(Id);
            string query = @"EditarVenta";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Id,
                Fecha = venta.Fecha,
                Hora = venta.Hora,
                Total = venta.Total
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            await verificarVentaExiste(Id);
            string query = @"EliminarVenta";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Id
            });
            return resultadoConsulta;
        }

        public async Task<IEnumerable<VentaResponse>> Obtener()
        {
            string query = @"ObtenerVentas";
            var resultadoConsulta = await _sqlConnection.QueryAsync<VentaResponse>(query);
            return resultadoConsulta;
        }

        public async Task<VentaResponse> Obtener(Guid Id)
        {
            string query = @"ObtenerVenta";
            var resultadoConsulta = await _sqlConnection.QueryAsync<VentaResponse>(query, new
            {
                Id = Id
            });
            return resultadoConsulta.FirstOrDefault();
        }

        #endregion

        #region Helpers

        private async Task verificarVentaExiste(Guid Id)
        {
            var resultado = await Obtener(Id);
            if (resultado == null)
                throw new Exception("Venta no encontrada.");
        }

        #endregion
    }
}
