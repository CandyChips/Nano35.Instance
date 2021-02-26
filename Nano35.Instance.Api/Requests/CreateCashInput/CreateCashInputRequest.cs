using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.CreateCashInput
{
    public class CreateCashInputRequest :
        IPipelineNode<
            ICreateCashInputRequestContract,
            ICreateCashInputResultContract>
    {
        private readonly IBus _bus;
        
        private readonly ICustomAuthStateProvider _auth;

        public CreateCashInputRequest(
            IBus bus,
            ICustomAuthStateProvider auth)
        {
            _bus = bus;
            _auth = auth;
        }
        
        private class CreateCashInputSuccessResultContract : 
            ICreateCashInputSuccessResultContract
        {
            
        }

        public async Task<ICreateCashInputResultContract> Ask(
            ICreateCashInputRequestContract input)
        {
            input.UpdaterId = _auth.CurrentUserId;
            
            // Configure request client of input type
            var client = _bus.CreateRequestClient<ICreateCashInputRequestContract>(TimeSpan.FromSeconds(10));
            
            // Receive response of processor magic
            var response = await client
                .GetResponse<
                    ICreateCashInputSuccessResultContract, 
                    ICreateCashInputErrorResultContract>(input);
            
            // Checking response status
            if (response.Is(out Response<ICreateCashInputSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<ICreateCashInputErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}