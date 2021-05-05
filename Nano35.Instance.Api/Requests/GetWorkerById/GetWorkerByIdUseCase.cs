using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetWorkerById
{
    public class GetWorkerByIdUseCase : UseCaseEndPointNodeBase<IGetWorkerByIdRequestContract, IGetWorkerByIdResultContract>
    {
        private readonly IBus _bus;
        public GetWorkerByIdUseCase(IBus bus) => _bus = bus;

        public override async Task<UseCaseResponse<IGetWorkerByIdResultContract>> Ask(IGetWorkerByIdRequestContract input) =>
            await new MasstransitUseCaseRequest<IGetWorkerByIdRequestContract, IGetWorkerByIdResultContract>(_bus, input)
                .GetResponse();
    }
}