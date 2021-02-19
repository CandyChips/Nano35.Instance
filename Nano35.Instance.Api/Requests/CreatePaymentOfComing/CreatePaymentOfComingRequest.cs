using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.CreatePaymentOfComing
{
    public class CreatePaymentOfComingRequest :
        IPipelineNode<ICreatePaymentOfComingRequestContract, ICreatePaymentOfComingResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;

        /// <summary>
        /// The request is accepted by the bus processing the request
        /// and auth provider to add context data to request
        /// </summary>
        public CreatePaymentOfComingRequest(
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
        public async Task<ICreatePaymentOfComingResultContract> Ask(ICreatePaymentOfComingRequestContract input)
        {
            // Configure request client of input type
            var client = _bus.CreateRequestClient<ICreatePaymentOfComingRequestContract>(TimeSpan.FromSeconds(10));
            
            // Receive response of processor magic
            var response = await client
                .GetResponse<ICreatePaymentOfComingSuccessResultContract, ICreatePaymentOfComingErrorResultContract>(input);
            
            // Checking response status
            if (response.Is(out Response<ICreatePaymentOfComingSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<ICreatePaymentOfComingErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}