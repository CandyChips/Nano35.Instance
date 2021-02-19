using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateInstancePhone
{
    public class UpdateInstancePhoneRequest :
        IPipelineNode<IUpdateInstancePhoneRequestContract, IUpdateInstancePhoneResultContract>
    {
        private readonly IBus _bus;

        /// <summary>
        /// The request is accepted by the bus processing the request
        /// </summary>
        public UpdateInstancePhoneRequest(
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
        public async Task<IUpdateInstancePhoneResultContract> Ask(IUpdateInstancePhoneRequestContract input)
        {
            // Configure request client of input type
            var client = _bus.CreateRequestClient<IUpdateInstancePhoneRequestContract>(TimeSpan.FromSeconds(10));
            
            // Receive response of processor magic
            var response = await client
                .GetResponse<IUpdateInstancePhoneSuccessResultContract, IUpdateInstancePhoneErrorResultContract>(input);
            
            // Checking response status
            if (response.Is(out Response<IUpdateInstancePhoneSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IUpdateInstancePhoneErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}