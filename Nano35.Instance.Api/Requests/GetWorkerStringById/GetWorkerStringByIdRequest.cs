using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetWorkerStringById
{
    public class GetWorkerStringByIdRequest :
        MasstransitRequest
        <IGetWorkerStringByIdRequestContract,
            IGetWorkerStringByIdResultContract,
            IGetWorkerStringByIdSuccessResultContract,
            IGetWorkerStringByIdErrorResultContract>
    {
        public GetWorkerStringByIdRequest(IBus bus, IGetWorkerStringByIdRequestContract request) : base(bus, request) {}
    }
}