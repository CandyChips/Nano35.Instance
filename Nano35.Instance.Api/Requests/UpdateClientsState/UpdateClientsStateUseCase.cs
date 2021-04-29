using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.UpdateClientsState
{
    public class UpdateClientsStateUseCase :
        EndPointNodeBase<
            IUpdateClientsStateRequestContract,
            IUpdateClientsStateResultContract>
    {
        private readonly IBus _bus;

        private readonly ICustomAuthStateProvider _auth;

        /// <summary>
        /// The request is accepted by the bus processing the request
        /// </summary>
        public UpdateClientsStateUseCase(
            IBus bus, 
            ICustomAuthStateProvider auth)
        {
            _bus = bus;
            _auth = auth;
        }
        
        /// <summary>
        /// Request sends to message bus when processor make magic with input
        /// 1. Generate client from context of request
        /// 2. Sends a request
        /// 3. Check and returns response
        /// 4? Throw exception if overtime
        /// </summary>
        public override async Task<IUpdateClientsStateResultContract> Ask(
            IUpdateClientsStateRequestContract input)
        {
            input.UpdaterId = _auth.CurrentUserId;

            return (await (new UpdateClientsStateRequest(_bus, input)).GetResponse());
        }
    }
}