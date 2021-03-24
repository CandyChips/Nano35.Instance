using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetClientStringById
{
    public class GetClientStringByIdUseCase :
        EndPointNodeBase<
            IGetClientStringByIdRequestContract, 
            IGetClientStringByIdResultContract>
    {
        private readonly IBus _bus;

        public GetClientStringByIdUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetClientStringByIdResultContract> Ask
            (IGetClientStringByIdRequestContract input) =>
            (await (new GetClientStringByIdRequest(_bus, input)).GetResponse());
    }
}