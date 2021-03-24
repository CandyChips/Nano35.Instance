using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllCurrentInstances
{
    public class GetAllCurrentInstancesRequet :
        MasstransitRequest
        <IGetAllInstancesRequestContract,
            IGetAllInstancesResultContract,
            IGetAllInstancesSuccessResultContract,
            IGetAllInstancesErrorResultContract>
    {
        public GetAllCurrentInstancesRequet(IBus bus, IGetAllInstancesRequestContract request) : base(bus, request) {}
    }
}