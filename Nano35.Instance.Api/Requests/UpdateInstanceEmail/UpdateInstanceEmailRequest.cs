using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateInstanceEmail
{
    public class UpdateInstanceEmailRequest :
        MasstransitRequest
        <IUpdateInstanceEmailRequestContract,
            IUpdateInstanceEmailResultContract,
            IUpdateInstanceEmailSuccessResultContract,
            IUpdateInstanceEmailErrorResultContract>
    {
        public UpdateInstanceEmailRequest(IBus bus, IUpdateInstanceEmailRequestContract request) : base(bus, request) {}
    }
}