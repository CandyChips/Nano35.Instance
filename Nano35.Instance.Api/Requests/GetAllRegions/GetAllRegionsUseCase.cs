using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllRegions
{
    public class GetAllRegionsUseCase : UseCaseEndPointNodeBase<IGetAllRegionsRequestContract, IGetAllRegionsSuccessResultContract>
    {
        private readonly IBus _bus;
        public GetAllRegionsUseCase(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<IGetAllRegionsSuccessResultContract>> Ask(IGetAllRegionsRequestContract input) =>
            await new MasstransitUseCaseRequest<IGetAllRegionsRequestContract, IGetAllRegionsSuccessResultContract>(_bus, input)
                .GetResponse();
    }
}