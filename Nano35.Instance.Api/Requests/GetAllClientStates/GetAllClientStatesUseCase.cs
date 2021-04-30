using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllClientStates
{
    public class GetAllClientStatesUseCase :
        EndPointNodeBase<IGetAllClientStatesRequestContract, IGetAllClientStatesResultContract>
    {
        private readonly IBus _bus;
        public GetAllClientStatesUseCase(IBus bus) => _bus = bus;

        public override async Task<IGetAllClientStatesResultContract> Ask(IGetAllClientStatesRequestContract input) =>
            await new MasstransitRequest<IGetAllClientStatesRequestContract, IGetAllClientStatesResultContract, IGetAllClientStatesSuccessResultContract, IGetAllClientStatesErrorResultContract>(_bus, input)
                .GetResponse();
        
    }
    
}