using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.CreateWorker
{
    public class CreateWorkerUseCase : UseCaseEndPointNodeBase<ICreateWorkerRequestContract, ICreateWorkerResultContract>
    {
        private readonly IBus _bus;
        public CreateWorkerUseCase(IBus bus) { _bus = bus; }
        public override async Task<UseCaseResponse<ICreateWorkerResultContract>> Ask(ICreateWorkerRequestContract input)
        {
            return await new MasstransitUseCaseRequest<ICreateWorkerRequestContract, ICreateWorkerResultContract>(_bus, input)
                .GetResponse();
        }
    }
}