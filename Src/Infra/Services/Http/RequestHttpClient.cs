using Infra.Dtos;
using Infra.Interfaces;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Infra.Services.Http
{
    public class RequestHttpClient : IRequestHttpClient
    {
        private readonly HttpClient _httpClient;
        public RequestHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResultadoBase<T>> GetAsync<T>(string url, string token)
        {
            try
            {
                ConfigurarToken(token);

                var httpResponseMessage = await _httpClient.GetAsync(url);
                var resultadoBase = ConverteHttpReponseParaResultadoBase<T>(httpResponseMessage);

                return resultadoBase;
            }
            catch (Exception ex)
            {
                return MontaResultadoBaseErro<T>(ex.Message); ;
            }
        }

        public async Task<ResultadoBase<T>> DeleteAsync<T>(string url, string token)
        {
            try
            {
                ConfigurarToken(token);

                var httpResponseMessage = await _httpClient.DeleteAsync(url);
                var resultadoBase = ConverteHttpReponseParaResultadoBase<T>(httpResponseMessage);

                return resultadoBase;
            }
            catch (Exception ex)
            {
                return MontaResultadoBaseErro<T>(ex.Message); ;
            }
        }

        public async Task<ResultadoBase<T>> PostAsync<T>(string url, object obj, string token)
        {
            try
            {
                ConfigurarToken(token);

                var httpResponseMessage = await _httpClient.PostAsJsonAsync(url, obj);
                var resultadoBase = ConverteHttpReponseParaResultadoBase<T>(httpResponseMessage);

                return resultadoBase;
            }
            catch (Exception ex)
            {
                return MontaResultadoBaseErro<T>(ex.Message); ;
            }
        }

        private static ResultadoBase<T> ConverteHttpReponseParaResultadoBase<T>(HttpResponseMessage httpResponseMessage)
        {
            string httpResponseString = httpResponseMessage.Content.ReadAsStringAsync().Result;
            var resultadoBase = JsonConvert.DeserializeObject<ResultadoBase<T>>(httpResponseString);

            resultadoBase ??= MontaResultadoBaseErro<T>($"StatusCode: {httpResponseMessage.StatusCode}");

            return resultadoBase;
        }

        private static ResultadoBase<T> MontaResultadoBaseErro<T>(string mensagem)
        {
            var resultadoBase = new ResultadoBase<T>();
            resultadoBase.Errors.Add(mensagem);

            return resultadoBase;
        }

        private void ConfigurarToken(string? token)
        {
            if (string.IsNullOrEmpty(token))
                _httpClient.DefaultRequestHeaders.Authorization = null;
            else
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token);
        }
    }
}
