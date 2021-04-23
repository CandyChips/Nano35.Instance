using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllUnits
{
    public class GetAllUnitsUseCase :
        EndPointNodeBase<
            IGetAllUnitsRequestContract, 
            IGetAllUnitsResultContract>
    {
        private readonly IBus _bus;

        public GetAllUnitsUseCase(IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetAllUnitsResultContract> Ask(IGetAllUnitsRequestContract input) =>
            (await (new GetAllUnitsRequest(_bus, input)).GetResponse());
    }
}