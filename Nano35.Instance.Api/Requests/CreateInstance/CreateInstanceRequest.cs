using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.CreateInstance
{
    public class CreateInstanceRequest :
        MasstransitRequest
        <ICreateInstanceRequestContract,
            ICreateInstanceResultContract,
            ICreateInstanceSuccessResultContract,
            ICreateInstanceErrorResultContract>
    {
        public CreateInstanceRequest(IBus bus, ICreateInstanceRequestContract request) : base(bus, request) {}
    }
}