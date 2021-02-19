using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateUnitsWorkingFormat
{
    public class UpdateUnitsWorkingFormatRequest :
        IPipelineNode<IUpdateUnitsWorkingFormatRequestContract, IUpdateUnitsWorkingFormatResultContract>
    {
        private readonly IBus _bus;

        /// <summary>
        /// The request is accepted by the bus processing the request
        /// </summary>
        public UpdateUnitsWorkingFormatRequest(
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
        public async Task<IUpdateUnitsWorkingFormatResultContract> Ask(IUpdateUnitsWorkingFormatRequestContract input)
        {
            // Configure request client of input type
            var client = _bus.CreateRequestClient<IUpdateUnitsWorkingFormatRequestContract>(TimeSpan.FromSeconds(10));
            
            // Receive response of processor magic
            var response = await client
                .GetResponse<IUpdateUnitsWorkingFormatSuccessResultContract, IUpdateUnitsWorkingFormatErrorResultContract>(input);
            
            // Checking response status
            if (response.Is(out Response<IUpdateUnitsWorkingFormatSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IUpdateUnitsWorkingFormatErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}