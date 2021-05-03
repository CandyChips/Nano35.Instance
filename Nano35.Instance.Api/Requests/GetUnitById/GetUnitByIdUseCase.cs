using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetUnitById
{
    public class GetUnitByIdUseCase : UseCaseEndPointNodeBase<IGetUnitByIdRequestContract, IGetUnitByIdSuccessResultContract>
    {
        private readonly IBus _bus;
        public GetUnitByIdUseCase(IBus bus) => _bus = bus;

        public override async Task<UseCaseResponse<IGetUnitByIdSuccessResultContract>> Ask(IGetUnitByIdRequestContract input) =>
            await new MasstransitUseCaseRequest<IGetUnitByIdRequestContract, IGetUnitByIdSuccessResultContract>(_bus, input)
                .GetResponse();

    }
}