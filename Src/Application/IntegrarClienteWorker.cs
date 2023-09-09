using Application.Interfaces;

namespace Application
{
    public class IntegrarClienteWorker : BackgroundService
    {
        private readonly ILogger<IntegrarClienteWorker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public IntegrarClienteWorker(IServiceScopeFactory scopeFactory, ILogger<IntegrarClienteWorker> logger)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (stoppingToken.IsCancellationRequested is false)
            {
                try
                {
                    await IniciarProcedimento();
                }
                catch (Exception ex)
                {
                    _logger.LogError("IntegrarClienteWorker: {erro}", ex.Message);
                }

                await Task.Delay(TimeSpan.FromMinutes(45), stoppingToken);
            }
        }

        private async Task IniciarProcedimento()
        {
            _logger.LogInformation("IntegrarClienteWorker: Iniciado");

            using (var scopo = _scopeFactory.CreateScope())
            {
                var clienteAppService = scopo.ServiceProvider.GetRequiredService<IClienteAppService>();
                await clienteAppService.IntegrarAsync();
            }

            _logger.LogInformation("IntegrarClienteWorker: Finalizado");
        }
    }
}