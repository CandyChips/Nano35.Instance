using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllRegions
{
    public class GetAllRegionsRequest:
        MasstransitRequest<
            IGetAllRegionsRequestContract,
            IGetAllRegionsResultContract,
            IGetAllRegionsSuccessResultContract,
            IGetAllRegionsErrorResultContract>
    {
        public GetAllRegionsRequest(IBus bus, IGetAllRegionsRequestContract request) : base(bus, request) {}
    }
}