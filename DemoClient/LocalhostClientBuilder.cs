using System;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;

namespace DemoClient
{
    public class LocalhostClientBuilder : ClientBuilder
    {
        public string ClusterId { get; set; }
        public string ServiceId { get; set; }
        public Action<ILoggingBuilder> ConfigLoggingBuilder { get; set; }

        public LocalhostClientBuilder(string clusterId, string serviceId)
        {
            ClusterId = clusterId;
            ServiceId = serviceId;
        }

        public new IClusterClient Build()
        {
            this.UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = ClusterId;
                    options.ServiceId = ServiceId;
                });
            if (ConfigLoggingBuilder != null)
            {
                this.ConfigureLogging(ConfigLoggingBuilder);
            }

            return base.Build();
        }
    }
}