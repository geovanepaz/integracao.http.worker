using Application;
using Application.Service;
using Application.Interfaces;
using Infra.Dapper;
using Infra.Interfaces;
using Infra.Repositories;
using Infra.Services.Http;
using System.Data;
using System.Data.SqlClient;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        var builder = new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
       .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}.json", optional: true, reloadOnChange: true).Build();

        //AppService
        services.AddScoped<IClienteAppService, ClienteAppService>();

        //Repository
        services.AddScoped<IClienteRepository, ClienteRepository>();


        //http
        services.AddScoped<ICadastroApiHttp, CadastroApiHttp>();
        services.AddHttpClient<IRequestHttpClient, RequestHttpClient>();

        //banco
        services.AddTransient<IDbConnection>(sp => new SqlConnection(builder.GetSection("ConnectionString").Get<string>()));
        services.AddScoped<IDapperUtil, DapperUtil>();

        //Worker
        services.AddHostedService<IntegrarClienteWorker>();
    })
    .Build();

host.Run();
