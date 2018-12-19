using System;
using System.Threading.Tasks;
using Orleans;

namespace OrleansDemo
{
    public class TemperatureSensorGrain : Grain, ITemperatureSensorGrain
    {
#pragma warning disable 1998
        public async Task SubmitTemperatureAsync(float temperature)
#pragma warning restore 1998
        {
            var grainId = this.GetPrimaryKeyLong();
            Console.WriteLine($"{grainId} received temperature : {temperature}");
        }
    }
}