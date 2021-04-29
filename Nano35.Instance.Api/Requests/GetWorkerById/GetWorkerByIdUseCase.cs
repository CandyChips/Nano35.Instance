using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetWorkerById
{
    public class GetWorkerByIdUseCase :
        EndPointNodeBase<
            IGetWorkerByIdRequestContract,
            IGetWorkerByIdResultContract>
    {
        private readonly IBus _bus;

        /// <summary>
        /// The request is accepted by the bus processing the request
        /// </summary>
        public GetWorkerByIdUseCase(
            IBus bus)
        {
            _bus = bus;
        }
        
        /// <summary>
        /// Request sends to message bus when processor make magic with input
        /// 1. Generate client from context of request
        /// 2. Sends a request
        /// 3. Check and returns response
        /// 4? Throw exception if overtime
        /// </summary>
        public override async Task<IGetWorkerByIdResultContract> Ask
            (IGetWorkerByIdRequestContract input) =>
            (await (new GetWorkerByIdRequest(_bus, input)).GetResponse());

    }
}