using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateInstanceName
{
    public class UpdateInstanceNameRequest :
        MasstransitRequest
        <IUpdateInstanceNameRequestContract,
            IUpdateInstanceNameResultContract,
            IUpdateInstanceNameSuccessResultContract,
            IUpdateInstanceNameErrorResultContract>
    {
        public UpdateInstanceNameRequest(IBus bus, IUpdateInstanceNameRequestContract request) : base(bus, request) {}
    }
}