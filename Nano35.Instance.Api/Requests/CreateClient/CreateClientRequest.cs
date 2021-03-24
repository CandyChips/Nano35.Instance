using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.CreateClient
{
    public class CreateClientRequest : 
        MasstransitRequest
        <ICreateClientRequestContract,
            ICreateClientResultContract,
            ICreateClientSuccessResultContract,
            ICreateClientErrorResultContract>
    {
        public CreateClientRequest(IBus bus, ICreateClientRequestContract request) : base(bus, request) {}
    }
}