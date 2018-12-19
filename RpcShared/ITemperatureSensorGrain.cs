using System.Threading.Tasks;
using Orleans;

namespace RpcShared
{
    // Note: be sure to install this nuget for Orleans generate code correctly:
    // https://www.nuget.org/packages/Microsoft.Orleans.CodeGenerator.MSBuild/
    public interface ITemperatureSensorGrain : IGrainWithIntegerKey
    {
        Task SubmitTemperatureAsync(float temperature);
    }
}