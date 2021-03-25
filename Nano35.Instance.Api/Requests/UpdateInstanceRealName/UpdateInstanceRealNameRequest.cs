using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateInstanceRealName
{
    public class UpdateInstanceRealNameRequest:
        MasstransitRequest
        <IUpdateInstanceRealNameRequestContract,
            IUpdateInstanceRealNameResultContract,
            IUpdateInstanceRealNameSuccessResultContract,
            IUpdateInstanceRealNameErrorResultContract>
    {
        public UpdateInstanceRealNameRequest(IBus bus, IUpdateInstanceRealNameRequestContract request) : base(bus, request) {}
    }
}