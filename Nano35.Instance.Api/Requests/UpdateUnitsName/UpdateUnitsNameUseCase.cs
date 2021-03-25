using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.UpdateUnitsName
{
    public class UpdateUnitsNameUseCase :
        EndPointNodeBase<
            IUpdateUnitsNameRequestContract,
            IUpdateUnitsNameResultContract>
    {
        private readonly IBus _bus;

        private readonly ICustomAuthStateProvider _auth;
        
        /// <summary>
        /// The request is accepted by the bus processing the request
        /// </summary>
        public UpdateUnitsNameUseCase(
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
        public override async Task<IUpdateUnitsNameResultContract> Ask(
            IUpdateUnitsNameRequestContract input)
        {
            input.UpdaterId = _auth.CurrentUserId;
            
            return (await (new UpdateUnitsNameRequest(_bus, input)).GetResponse());

        }
    }
}