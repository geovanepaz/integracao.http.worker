using Infra.Dtos;
using Infra.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Infra.Services.Http
{
    public class CadastroApiHttp : ICadastroApiHttp
    {
        private readonly IRequestHttpClient _requestHttpClient;
        private readonly IConfiguration _configuration;

        private readonly ILogger<ICadastroApiHttp> _logger;
        private readonly string _urlBase;

        private static readonly string _nomeApi = "CadastroAPi";


        public CadastroApiHttp(ILogger<ICadastroApiHttp> logger, IConfiguration configuration, IRequestHttpClient requestHttpClient)
        {
            _requestHttpClient = requestHttpClient;
            _configuration = configuration;

            _logger = logger;
            _urlBase = BuscarUrlBase();
        }

        private string BuscarUrlBase()
        {
            var urlBase = _configuration.GetSection(_nomeApi).GetSection("urlBase").Value;
            if (urlBase is null)
                throw new ArgumentException("Não foi encontrado a url base: CadastroAPi");

            return urlBase;
        }

        public async Task IntegrarClienteAsync(ClienteDto clienteDto)
        {
            var url = BuscarUrl("EndPontIntegrarCliente");

            var resultado = await _requestHttpClient.PostAsync<dynamic>(url, clienteDto, string.Empty);

            if (resultado.Success)
                _logger.LogInformation("IntegrarClienteWorker: Cliente {nome} id: {id} integrado com sucesso.", clienteDto.NomeCompleto, clienteDto.IdIntegracao);
            else
                _logger.LogError("IntegrarClienteWorker: Cliente {nome} id: {id} não integrado. Mensagem de erro: {erro}", clienteDto.NomeCompleto, clienteDto.IdIntegracao, resultado.Errors?.FirstOrDefault());
        }

        private string BuscarUrl(string nomeSecao)
        {
            var endPoint = _configuration.GetSection(_nomeApi).GetSection(nomeSecao).Value;
            if (endPoint is null)
                throw new ArgumentException($"Não foi encontrado o endPoint '{nomeSecao}' nas configurações da aplicação.");

            return _urlBase + endPoint;
        }
    }
}
