namespace Infra.Interfaces
{
    public interface IDapperUtil
    {
        Task<IEnumerable<T>> RunQueryAsync<T>(string query, object? parametros = null, int timeout = 30);
        Task<T> QueryFirstOrDefaultAsync<T>(string query, object? parametros = null, int timeout = 30);
        Task InsertAsync(string query, object parametros, int timeout = 30);
    }
}
