using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.CreateCashOutput
{
    public class CreateCashOutputRequest :
        IPipelineNode<
            ICreateCashOutputRequestContract,
            ICreateCashOutputResultContract>
    {
        private readonly IBus _bus;
        
        private readonly ICustomAuthStateProvider _auth;

        public CreateCashOutputRequest(
            IBus bus,
            ICustomAuthStateProvider auth)
        {
            _bus = bus;
            _auth = auth;
        }
        
        private class CreateCashOutputSuccessResultContract : 
            ICreateCashOutputSuccessResultContract
        {
            
        }
        
        public async Task<ICreateCashOutputResultContract> Ask(
            ICreateCashOutputRequestContract input)
        {
            input.UpdaterId = _auth.CurrentUserId;
            
            // Configure request client of input type
            var client = _bus.CreateRequestClient<ICreateCashOutputRequestContract>(TimeSpan.FromSeconds(10));
            
            // Receive response of processor magic
            var response = await client
                .GetResponse<
                    ICreateCashOutputSuccessResultContract, 
                    ICreateCashOutputErrorResultContract>(input);
            
            // Checking response status
            if (response.Is(out Response<ICreateCashOutputSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<ICreateCashOutputErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}