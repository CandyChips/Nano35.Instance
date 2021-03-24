using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetInstanceStringById
{
    public class GetInstanceStringByIdUseCase :
        EndPointNodeBase<
            IGetInstanceStringByIdRequestContract, 
            IGetInstanceStringByIdResultContract>
    {
        private readonly IBus _bus;

        public GetInstanceStringByIdUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetInstanceStringByIdResultContract> Ask
            (IGetInstanceStringByIdRequestContract input) =>
            (await (new GetInstanceStringByIdRequest(_bus, input)).GetResponse());

    }
}