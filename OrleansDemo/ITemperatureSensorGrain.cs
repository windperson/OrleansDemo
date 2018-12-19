using System.Threading.Tasks;
using Orleans;

namespace OrleansDemo
{
    public interface ITemperatureSensorGrain : IGrainWithIntegerKey
    {
        Task SubmitTemperatureAsync(float temperature);
    }
}