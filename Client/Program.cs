using Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;

[assembly: GenerateCodeForDeclaringAssembly(typeof(Grains.HelloWorldA))]
class Program
{
    static async Task Main(string[] args)
    {
        var host = new HostBuilder()
            .ConfigureServices(services =>
            {
                services.AddOrleansClient(clientBuilder =>
                {
                    clientBuilder.UseLocalhostClustering()
                        .Configure<ClusterOptions>(options =>
                        {
                            options.ClusterId = "dev";
                            options.ServiceId = "OrleansBasics";
                        });
                });
            })
            .Build();

        await host.StartAsync();

        var client = host.Services.GetRequiredService<IClusterClient>();
        var grain = client.GetGrain<IHelloWorldB>(0);
        
    }
}