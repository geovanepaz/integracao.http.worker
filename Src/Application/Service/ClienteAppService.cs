using Application.Interfaces;
using Infra.Interfaces;

namespace Application.Service
{
    public class ClienteAppService : IClienteAppService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly ICadastroApiHttp _cadastroApiHttp;

        private readonly ILogger<IClienteAppService> _logger;

        public ClienteAppService(ILogger<IClienteAppService> logger, IClienteRepository clienteRepository, ICadastroApiHttp cadastroApiHttp)
        {
            _clienteRepository = clienteRepository;
            _cadastroApiHttp = cadastroApiHttp;

            _logger = logger;
        }

        public async Task IntegrarAsync()
        {
            var clienteDtoLista = await _clienteRepository.BuscarClientesPendenteIntegracaoAsync();
            if (clienteDtoLista.Any() is false)
                return;

            _logger.LogInformation("IntegrarClienteWorker: Quantidade de clientes encontrado: {contador} ", clienteDtoLista.Count());

            foreach (var clienteDto in clienteDtoLista)
                await _cadastroApiHttp.IntegrarClienteAsync(clienteDto);
        }
    }
}
