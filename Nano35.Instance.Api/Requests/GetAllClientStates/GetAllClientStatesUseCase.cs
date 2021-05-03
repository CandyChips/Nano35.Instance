using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllClientStates
{
    public class GetAllClientStatesUseCase : UseCaseEndPointNodeBase<IGetAllClientStatesRequestContract, IGetAllClientStatesSuccessResultContract>
    {
        private readonly IBus _bus;
        public GetAllClientStatesUseCase(IBus bus) => _bus = bus;

        public override async Task<UseCaseResponse<IGetAllClientStatesSuccessResultContract>> Ask(IGetAllClientStatesRequestContract input) =>
            await new MasstransitUseCaseRequest<IGetAllClientStatesRequestContract, IGetAllClientStatesSuccessResultContract>(_bus, input)
                .GetResponse();
        
    }
    
}