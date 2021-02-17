using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.CreateWorker
{
    public class CreateWorkerRequest :
        IPipelineNode<ICreateWorkerRequestContract, ICreateWorkerResultContract>
    {
        private readonly ICustomAuthStateProvider _auth;
        private readonly IBus _bus;
        
        /// <summary>
        /// The request is accepted by the bus processing the request
        /// </summary>
        public CreateWorkerRequest(
            IBus bus, 
            ICustomAuthStateProvider auth)
        {
            _bus = bus;
            _auth = auth;
        }
        
        /// <summary>
        /// Request sends to messa ge bus when processor make magic with input
        /// 1. Generate client from context of request
        /// 2. Sends a request
        /// 3. Check and returns response
        /// 4? Throw exception if overtime
        /// </summary>
        public async Task<ICreateWorkerResultContract> Ask(ICreateWorkerRequestContract input)
        {
            input.Phone = PhoneConverter.RuPhoneConverter(input.Phone);

            // Configure request client of input type
            var client = _bus.CreateRequestClient<ICreateWorkerRequestContract>(TimeSpan.FromSeconds(10));
            
            // Receive response of processor magic
            var response = await client
                .GetResponse<ICreateWorkerSuccessResultContract, ICreateWorkerErrorResultContract>(input);
            
            // Checking response status
            if (response.Is(out Response<ICreateWorkerSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<ICreateWorkerErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}