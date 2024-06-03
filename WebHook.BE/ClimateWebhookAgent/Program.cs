using ClimateWebhookAgent.AppHost;
using ClimateWebhookAgent.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder().ConfigureServices((context, service) => {
        service.AddSingleton<IAppHost, AppHost>();
    service.AddDbContext<ClimateContext>(options =>
    {
        options.UseSqlServer(context.Configuration.GetConnectionString("myConn"));
    });
    service.AddHttpClient();
}).Build();

var appHost = host.Services.GetRequiredService<IAppHost>();
appHost.Run();