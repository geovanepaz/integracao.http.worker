using Dapper;
using Infra.Interfaces;
using System.Data;

namespace Infra.Dapper
{
    public class DapperUtil : IDapperUtil
    {
        private readonly IDbConnection _sqlConnection;

        public DapperUtil(IDbConnection dbConnection)
        {
            _sqlConnection = dbConnection;
        }

        public async Task InsertAsync(string query, object parametros, int timeout = 30)
        {
            await _sqlConnection.ExecuteAsync(query, parametros, commandTimeout: timeout);
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string query, object? parametros = null, int timeout = 30)
        {
            return await _sqlConnection.QueryFirstOrDefaultAsync<T>(query, parametros, commandTimeout: timeout);
        }

        public async Task<IEnumerable<T>> RunQueryAsync<T>(string query, object? parametros = null, int timeout = 30)
        {
            return await _sqlConnection.QueryAsync<T>(query, parametros, commandTimeout: timeout);
        }
    }
}
