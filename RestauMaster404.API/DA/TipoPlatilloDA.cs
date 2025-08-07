using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DA
{
    public class TipoPlatilloDA : ITipoPlatilloDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public TipoPlatilloDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        #region Operaciones

        public async Task<Guid> Agregar(TipoPlatilloRequest tipo)
        {
            string query = @"AgregarTipoPlatillo";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Guid.NewGuid(),
                Nombre = tipo.Nombre
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Editar(Guid Id, TipoPlatilloRequest tipo)
        {
            await verificarTipoPlatilloExiste(Id);
            string query = @"EditarTipoPlatillo";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Id,
                Nombre = tipo.Nombre
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            await verificarTipoPlatilloExiste(Id);
            string query = @"EliminarTipoPlatillo";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Id
            });
            return resultadoConsulta;
        }

        public async Task<IEnumerable<TipoPlatilloResponse>> Obtener()
        {
            string query = @"ObtenerTiposPlatillos";
            var resultadoConsulta = await _sqlConnection.QueryAsync<TipoPlatilloResponse>(query);
            return resultadoConsulta;
        }

        public async Task<TipoPlatilloResponse> Obtener(Guid Id)
        {
            string query = @"ObtenerTipoPlatillo";
            var resultadoConsulta = await _sqlConnection.QueryAsync<TipoPlatilloResponse>(query, new { Id });
            return resultadoConsulta.FirstOrDefault();
        }

        #endregion

        #region Helpers

        private async Task verificarTipoPlatilloExiste(Guid Id)
        {
            TipoPlatilloResponse? resultado = await Obtener(Id);
            if (resultado == null)
                throw new Exception("Tipo de platillo no encontrado.");
        }

        #endregion
    }
}
