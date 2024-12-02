using Grains;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans.Configuration;

[assembly: GenerateCodeForDeclaringAssembly(typeof(Grains.HelloWorldGrain))]
public class Program
{
    
    public static async Task Main(string[] args)
    {
        var host = new HostBuilder()
            .UseOrleans(siloBuilder =>
            {
                siloBuilder.UseLocalhostClustering()
                    
                    .Configure<ClusterOptions>(options =>
                    {
                        options.ClusterId = "dev";
                        options.ServiceId = "OrleansBasics";
                    });
                
                siloBuilder.UseDashboard();
            })
            .Build();

        await host.StartAsync();
        
        var grainFactory = host.Services.GetRequiredService<IGrainFactory>();
        var grain = grainFactory.GetGrain<IHelloWorldGrain>(Guid.Empty);
        var result = await grain.SayHello("Hello");
    }
}