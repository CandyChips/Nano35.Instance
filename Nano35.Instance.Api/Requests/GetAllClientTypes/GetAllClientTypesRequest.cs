using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllClientTypes
{
    public class GetAllClientTypesRequest :
        IPipelineNode<IGetAllClientTypesRequestContract, IGetAllClientTypesResultContract>
    {
        private readonly IBus _bus;

        /// <summary>
        /// The request is accepted by the bus processing the request
        /// </summary>
        public GetAllClientTypesRequest(
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
        public async Task<IGetAllClientTypesResultContract> Ask(IGetAllClientTypesRequestContract input)
        {
            // Configure request client of input type
            var client = _bus.CreateRequestClient<IGetAllClientTypesRequestContract>(TimeSpan.FromSeconds(10));
            
            // Receive response of processor magic
            var response = await client
                .GetResponse<IGetAllClientTypesSuccessResultContract, IGetAllClientTypesErrorResultContract>(input);
            
            // Checking response status
            if (response.Is(out Response<IGetAllClientTypesSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetAllClientTypesErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}