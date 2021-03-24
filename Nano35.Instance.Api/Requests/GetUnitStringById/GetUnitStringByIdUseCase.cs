using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetUnitStringById
{
    public class GetUnitStringByIdUseCase :
        EndPointNodeBase<
            IGetUnitStringByIdRequestContract, 
            IGetUnitStringByIdResultContract>
    {
        private readonly IBus _bus;

        public GetUnitStringByIdUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetUnitStringByIdResultContract> Ask
            (IGetUnitStringByIdRequestContract input) =>
            (await (new GetUnitStringByIdRequest(_bus, input)).GetResponse());

    }
}