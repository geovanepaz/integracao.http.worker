using Infra.Dtos;

namespace Infra.Interfaces
{
    public interface IRequestHttpClient
    {
        Task<ResultadoBase<T>> PostAsync<T>(string url, object obj, string token);
        Task<ResultadoBase<T>> GetAsync<T>(string url, string token);
        Task<ResultadoBase<T>> DeleteAsync<T>(string url, string token);
    }
}
