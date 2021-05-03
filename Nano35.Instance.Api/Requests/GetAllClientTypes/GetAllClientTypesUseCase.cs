using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllClientTypes
{
    public class GetAllClientTypesUseCase : UseCaseEndPointNodeBase<IGetAllClientTypesRequestContract, IGetAllClientTypesSuccessResultContract>
    {
        private readonly IBus _bus;
        public GetAllClientTypesUseCase(IBus bus) => _bus = bus;
        public override async Task<UseCaseResponse<IGetAllClientTypesSuccessResultContract>> Ask(IGetAllClientTypesRequestContract input) => 
            await new MasstransitUseCaseRequest<IGetAllClientTypesRequestContract, IGetAllClientTypesSuccessResultContract>(_bus, input)
                .GetResponse();
    }
}