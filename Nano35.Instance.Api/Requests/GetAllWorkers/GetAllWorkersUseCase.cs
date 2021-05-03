using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllWorkers
{
    public class GetAllWorkersUseCase : UseCaseEndPointNodeBase<IGetAllWorkersRequestContract, IGetAllWorkersSuccessResultContract>
    {
        private readonly IBus _bus;
        public GetAllWorkersUseCase(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<IGetAllWorkersSuccessResultContract>> Ask(IGetAllWorkersRequestContract input) =>
            await new MasstransitUseCaseRequest<IGetAllWorkersRequestContract, IGetAllWorkersSuccessResultContract>(_bus, input)
                .GetResponse();
    }
}