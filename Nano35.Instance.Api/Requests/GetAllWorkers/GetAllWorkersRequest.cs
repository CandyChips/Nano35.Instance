using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllWorkers
{
    public class GetAllWorkersRequest :
        MasstransitRequest
        <IGetAllWorkersRequestContract,
            IGetAllWorkersResultContract,
            IGetAllWorkersSuccessResultContract,
            IGetAllWorkersErrorResultContract>
    {
        public GetAllWorkersRequest(IBus bus, IGetAllWorkersRequestContract request) : base(bus, request) {}
    }
}