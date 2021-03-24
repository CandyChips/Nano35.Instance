using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllInstances
{
    public class GetAllInstancesRequest :
        MasstransitRequest
        <IGetAllInstancesRequestContract,
            IGetAllInstancesResultContract,
            IGetAllInstancesSuccessResultContract,
            IGetAllInstancesErrorResultContract>
    {
        public GetAllInstancesRequest(IBus bus, IGetAllInstancesRequestContract request) : base(bus, request) {}
    }
}