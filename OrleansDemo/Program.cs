using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;

namespace OrleansDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var siloBuilder = new SiloHostBuilder().UseLocalhostClustering()
                    .Configure<ClusterOptions>(options =>
                    {
                        options.ClusterId = "dev";
                        options.ServiceId = "Orleans2GettingStarted";
                    })
                    .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                    .ConfigureLogging(logging =>
                    {
                        logging.AddConsole();
                        logging.AddDebug();
                    });

                using (var host = siloBuilder.Build())
                {
                    await host.StartAsync();
                    Console.WriteLine("Service started, press any key to create client to connect it.");
                    Console.ReadKey();

                    var clientBuilder = new ClientBuilder().UseLocalhostClustering()
                        .Configure<ClusterOptions>(options =>
                        {
                            options.ClusterId = "dev";
                            options.ServiceId = "Orleans2GettingStarted";
                        })
                        .ConfigureLogging(logging =>
                        {
                            logging.AddConsole();
                            logging.AddDebug();
                        });

                    using (var client = clientBuilder.Build())
                    {
                        await client.Connect();
                        Console.WriteLine("Client connected, press any key to create grain.");
                        Console.ReadKey();

                        var sensor = client.GetGrain<ITemperatureSensorGrain>(123);
                        await sensor.SubmitTemperatureAsync(32.5f);
                        Console.WriteLine("Client sent message successfully, press any key to close client.");
                        await client.Close();
                    }

                    Console.WriteLine("client disposed, press any key to stop host");
                    Console.ReadKey();
                    await host.StopAsync();
                }
                Console.WriteLine("host stopped, press any key to exit.");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
