using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DA
{
    public class PlatilloDA : IPlatilloDA
    {
        private readonly IRepositorioDapper _repositorioDapper;
        private readonly SqlConnection _sqlConnection;

        public PlatilloDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }

        #region Operaciones

        public async Task<Guid> Agregar(PlatilloRequest platillo)
        {
            string query = @"AgregarPlatillo";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Guid.NewGuid(),
                IdTipoPlatillo = platillo.IdTipoPlatillo,
                Nombre = platillo.Nombre,
                Precio = platillo.Precio,
                Stock = platillo.Stock,
                IdEstado = platillo.IdEstado
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Editar(Guid Id, PlatilloRequest platillo)
        {
            await verificarPlatilloExiste(Id);
            string query = @"EditarPlatillo";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Id,
                IdTipoPlatillo = platillo.IdTipoPlatillo,
                Nombre = platillo.Nombre,
                Precio = platillo.Precio,
                Stock = platillo.Stock,
                IdEstado = platillo.IdEstado
            });
            return resultadoConsulta;
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            await verificarPlatilloExiste(Id);
            string query = @"EliminarPlatillo";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Id
            });
            return resultadoConsulta;
        }

        public async Task<IEnumerable<PlatilloResponse>> Obtener()
        {
            string query = @"ObtenerPlatillos";
            var resultadoConsulta = await _sqlConnection.QueryAsync<PlatilloResponse>(query);
            return resultadoConsulta;
        }

        public async Task<PlatilloDetalle> Obtener(Guid Id)
        {
            string query = @"ObtenerPlatillo";
            var resultadoConsulta = await _sqlConnection.QueryAsync<PlatilloDetalle>(query, new
            {
                Id = Id
            });
            return resultadoConsulta.FirstOrDefault();
        }

        #endregion

        #region Helpers

        private async Task verificarPlatilloExiste(Guid Id)
        {
            PlatilloDetalle? resultado = await Obtener(Id);
            if (resultado == null)
                throw new Exception("Platillo no encontrado.");
        }

        #endregion
    }
}