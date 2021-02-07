using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.CreateInstance
{
    public class CreateInstanceRequest :
        IPipelineNode<ICreateInstanceRequestContract, ICreateInstanceResultContract>
    {
        private readonly IBus _bus;
        private readonly ICustomAuthStateProvider _auth;

        public CreateInstanceRequest(
            IBus bus, 
            ICustomAuthStateProvider auth)
        {
            _bus = bus;
            _auth = auth;
        }
        
        public async Task<ICreateInstanceResultContract> Ask(ICreateInstanceRequestContract input)
        {
            input.UserId = _auth.CurrentUserId;
            
            var client = _bus.CreateRequestClient<ICreateInstanceRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<ICreateInstanceSuccessResultContract, ICreateInstanceErrorResultContract>(input);
            
            if (response.Is(out Response<ICreateInstanceSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<ICreateInstanceErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}