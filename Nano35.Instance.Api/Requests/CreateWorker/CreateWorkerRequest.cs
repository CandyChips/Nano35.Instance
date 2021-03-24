using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.CreateWorker
{
    public class CreateWorkerRequest :
        MasstransitRequest
        <ICreateWorkerRequestContract,
            ICreateWorkerResultContract,
            ICreateWorkerSuccessResultContract,
            ICreateWorkerErrorResultContract>
    {
        public CreateWorkerRequest(IBus bus, ICreateWorkerRequestContract request) : base(bus, request) {}
    }
}