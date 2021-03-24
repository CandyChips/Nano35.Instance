using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetWorkerStringById
{
    public class GetWorkerStringByIdUseCase :
        EndPointNodeBase<IGetWorkerStringByIdRequestContract, IGetWorkerStringByIdResultContract>
    {
        private readonly IBus _bus;

        public GetWorkerStringByIdUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        public override async Task<IGetWorkerStringByIdResultContract> Ask
            (IGetWorkerStringByIdRequestContract input) =>
            (await (new GetWorkerStringByIdRequest(_bus, input)).GetResponse());
    }
}