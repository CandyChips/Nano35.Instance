using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllUnitTypes
{
    public class GetAllUnitTypesUseCase : UseCaseEndPointNodeBase<IGetAllUnitTypesRequestContract, IGetAllUnitTypesResultContract>
    {
        private readonly IBus _bus;
        public GetAllUnitTypesUseCase(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<IGetAllUnitTypesResultContract>> Ask(IGetAllUnitTypesRequestContract input) => 
            await new MasstransitUseCaseRequest<IGetAllUnitTypesRequestContract, IGetAllUnitTypesResultContract>(_bus, input)
                .GetResponse();
    }
}