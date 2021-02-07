using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetInstanceById
{
    public class GetInstanceByIdRequest :
        IPipelineNode<IGetInstanceByIdRequestContract, IGetInstanceByIdResultContract>
    {
        private readonly IBus _bus;

        public GetInstanceByIdRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IGetInstanceByIdResultContract> Ask(IGetInstanceByIdRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetInstanceByIdRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetInstanceByIdSuccessResultContract, IGetInstanceByIdErrorResultContract>(input);
            
            if (response.Is(out Response<IGetInstanceByIdSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetInstanceByIdErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}