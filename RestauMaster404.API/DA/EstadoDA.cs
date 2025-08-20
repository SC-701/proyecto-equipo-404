using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DA
{
    public class EstadoDA : IEstadoDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;

        public EstadoDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        #region Operaciones

        public async Task<Guid> Agregar(EstadoRequest estado)
        {
            string query = @"AgregarEstado";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Guid.NewGuid(),
                Nombre = estado.Nombre
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Editar(Guid Id, EstadoRequest estado)
        {
            await verificarEstadoExiste(Id);
            string query = @"EditarEstado";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Id,
                Nombre = estado.Nombre
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            await verificarEstadoExiste(Id);
            string query = @"EliminarEstado";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Id
            });
            return resultadoConsulta;
        }

        public async Task<IEnumerable<EstadoResponse>> Obtener()
        {
            string query = @"ObtenerEstados";
            var resultadoConsulta = await _sqlConnection.QueryAsync<EstadoResponse>(query);
            return resultadoConsulta;
        }

        public async Task<EstadoResponse> Obtener(Guid Id)
        {
            string query = @"ObtenerEstado";
            var resultadoConsulta = await _sqlConnection.QueryAsync<EstadoResponse>(query, new { Id = Id });
            return resultadoConsulta.FirstOrDefault();
        }

        #endregion

        #region Helpers

        private async Task verificarEstadoExiste(Guid Id)
        {
            EstadoResponse? resultadoConsultaEstado = await Obtener(Id);
            if (resultadoConsultaEstado == null)
                throw new Exception("Estado no encontrado.");
        }

        #endregion
    }
}