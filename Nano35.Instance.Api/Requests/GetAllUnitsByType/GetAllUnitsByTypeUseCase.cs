using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllUnitsByType
{
    public class GetAllUnitsByTypeUseCase : UseCaseEndPointNodeBase<IGetAllUnitsByTypeRequestContract, IGetAllUnitsByTypeResultContract>
    {
        private readonly IBus _bus;
        public GetAllUnitsByTypeUseCase(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<IGetAllUnitsByTypeResultContract>> Ask(IGetAllUnitsByTypeRequestContract input) =>
            await new MasstransitUseCaseRequest<IGetAllUnitsByTypeRequestContract, IGetAllUnitsByTypeResultContract>(_bus, input)
                .GetResponse();
    }
}