using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateInstanceInfo
{
    public class UpdateInstanceInfoRequest :
        MasstransitRequest
        <IUpdateInstanceInfoRequestContract,
            IUpdateInstanceInfoResultContract,
            IUpdateInstanceInfoSuccessResultContract,
            IUpdateInstanceInfoErrorResultContract>
    {
        public UpdateInstanceInfoRequest(IBus bus, IUpdateInstanceInfoRequestContract request) : base(bus, request) {}
    }
}