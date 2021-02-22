using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.UpdateUnitsType
{
    public class UpdateUnitsTypeRequest :
        IPipelineNode<
            IUpdateUnitsTypeRequestContract,
            IUpdateUnitsTypeResultContract>
    {
        private readonly IBus _bus;

        private readonly ICustomAuthStateProvider _auth;

        /// <summary>
        /// The request is accepted by the bus processing the request
        /// </summary>
        public UpdateUnitsTypeRequest(
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
        public async Task<IUpdateUnitsTypeResultContract> Ask(
            IUpdateUnitsTypeRequestContract input)
        {
            input.UpdaterId = _auth.CurrentUserId;
            
            // Configure request client of input type
            var client = _bus.CreateRequestClient<IUpdateUnitsTypeRequestContract>(TimeSpan.FromSeconds(10));
            
            // Receive response of processor magic
            var response = await client
                .GetResponse<IUpdateUnitsTypeSuccessResultContract, IUpdateUnitsTypeErrorResultContract>(input);
            
            // Checking response status
            if (response.Is(out Response<IUpdateUnitsTypeSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IUpdateUnitsTypeErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}