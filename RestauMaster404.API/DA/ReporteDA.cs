using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos.Reportes;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DA
{
    public class ReporteDA : IReporteDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;
    public ReporteDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        #region Operaciones
        public async Task<IEnumerable<ConteoPorTipoPlatillo>> ConteoPorTipoPlatillosAsync(bool soloDisponibles)
        {
            string query = @"sp_ConteoPlatillosPorTipo";
            var resultadoConsulta = await _sqlConnection.QueryAsync<ConteoPorTipoPlatillo>(
                query,
                new { SoloDisponibles = soloDisponibles },
                commandType: System.Data.CommandType.StoredProcedure
                );
            return resultadoConsulta;
        }
        #endregion
    }
}