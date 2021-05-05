using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllClientStates
{
    public class GetAllClientStatesUseCase : UseCaseEndPointNodeBase<IGetAllClientStatesRequestContract, IGetAllClientStatesResultContract>
    {
        private readonly IBus _bus;
        public GetAllClientStatesUseCase(IBus bus) => _bus = bus;

        public override async Task<UseCaseResponse<IGetAllClientStatesResultContract>> Ask(IGetAllClientStatesRequestContract input) =>
            await new MasstransitUseCaseRequest<IGetAllClientStatesRequestContract, IGetAllClientStatesResultContract>(_bus, input)
                .GetResponse();
        
    }
    
}