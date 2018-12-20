using System;
using System.Net;
using Microsoft.Extensions.Logging;
using Orleans.Configuration;
using Orleans.Hosting;

namespace DemoSilo
{
    public class LocalhostSiloBuilder : SiloHostBuilder
    {
        public string ClusterId { get; set; }
        public string ServiceId { get; set; }
        public Action<ILoggingBuilder> ConfigLoggingBuilder { get; set; }

        public LocalhostSiloBuilder(string clusterId, string serviceId)
        {
            ClusterId = clusterId;
            ServiceId = serviceId;
        }

        public new ISiloHost Build()
        {
            this.UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = ClusterId;
                    options.ServiceId = ServiceId;
                })
                .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                .ConfigureLogging(ConfigLoggingBuilder);

            return base.Build();
        }
    }
}