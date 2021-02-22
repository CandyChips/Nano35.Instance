using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.UpdateClientsType
{
    public class UpdateClientsTypeRequest :
        IPipelineNode<
            IUpdateClientsTypeRequestContract, 
            IUpdateClientsTypeResultContract>
    {
        private readonly IBus _bus;

        private readonly ICustomAuthStateProvider _auth;

        /// <summary>
        /// The request is accepted by the bus processing the request
        /// </summary>
        public UpdateClientsTypeRequest(
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
        public async Task<IUpdateClientsTypeResultContract> Ask(
            IUpdateClientsTypeRequestContract input)
        {
            input.UpdaterId = _auth.CurrentUserId;

            // Configure request client of input type
            var client = _bus.CreateRequestClient<IUpdateClientsTypeRequestContract>(TimeSpan.FromSeconds(10));
            
            // Receive response of processor magic
            var response = await client
                .GetResponse<IUpdateClientsTypeSuccessResultContract, IUpdateClientsTypeErrorResultContract>(input);
            
            // Checking response status
            if (response.Is(out Response<IUpdateClientsTypeSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IUpdateClientsTypeErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}