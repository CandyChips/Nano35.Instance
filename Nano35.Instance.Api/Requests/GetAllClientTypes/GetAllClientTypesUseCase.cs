using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllClientTypes
{
    public class GetAllClientTypesUseCase :
        EndPointNodeBase<
            IGetAllClientTypesRequestContract,
            IGetAllClientTypesResultContract>
    {
        private readonly IBus _bus;

        public GetAllClientTypesUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetAllClientTypesResultContract> Ask(
            IGetAllClientTypesRequestContract input) => 
            (await (new GetAllClientTypesRequest(_bus, input)).GetResponse());
    }
}