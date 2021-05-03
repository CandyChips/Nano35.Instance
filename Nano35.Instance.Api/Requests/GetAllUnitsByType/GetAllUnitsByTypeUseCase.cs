using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllUnitsByType
{
    public class GetAllUnitsByTypeUseCase : UseCaseEndPointNodeBase<IGetAllUnitsByTypeRequestContract, IGetAllUnitsByTypeSuccessResultContract>
    {
        private readonly IBus _bus;
        public GetAllUnitsByTypeUseCase(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<IGetAllUnitsByTypeSuccessResultContract>> Ask(IGetAllUnitsByTypeRequestContract input) =>
            await new MasstransitUseCaseRequest<IGetAllUnitsByTypeRequestContract, IGetAllUnitsByTypeSuccessResultContract>(_bus, input)
                .GetResponse();
    }
}