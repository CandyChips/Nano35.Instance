using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateInstanceRegion
{
    public class UpdateInstanceRegionRequest :
        MasstransitRequest
        <IUpdateInstanceRegionRequestContract,
            IUpdateInstanceRegionResultContract,
            IUpdateInstanceRegionSuccessResultContract,
            IUpdateInstanceRegionErrorResultContract>
    {
        public UpdateInstanceRegionRequest(IBus bus, IUpdateInstanceRegionRequestContract request) : base(bus, request) {}
    }
}