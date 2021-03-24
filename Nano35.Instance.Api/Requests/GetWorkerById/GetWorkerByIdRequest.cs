using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetWorkerById
{
    public class GetWorkerByIdRequest :
        MasstransitRequest
        <IGetWorkerByIdRequestContract,
            IGetWorkerByIdResultContract,
            IGetWorkerByIdSuccessResultContract,
            IGetWorkerByIdErrorResultContract>
    {
        public GetWorkerByIdRequest(IBus bus, IGetWorkerByIdRequestContract request) : base(bus, request) {}
    }
}