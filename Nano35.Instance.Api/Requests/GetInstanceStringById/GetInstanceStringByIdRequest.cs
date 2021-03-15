using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetInstanceStringById
{
    public class GetInstanceStringByIdRequest :
        IPipelineNode<IGetInstanceStringByIdRequestContract, IGetInstanceStringByIdResultContract>
    {
        private readonly IBus _bus;

        public GetInstanceStringByIdRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IGetInstanceStringByIdResultContract> Ask(IGetInstanceStringByIdRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetInstanceStringByIdRequestContract>(TimeSpan.FromSeconds(10));
            var response = await client
                .GetResponse<IGetInstanceStringByIdSuccessResultContract, IGetInstanceStringByIdErrorResultContract>(input);
            if (response.Is(out Response<IGetInstanceStringByIdSuccessResultContract> successResponse))
                return successResponse.Message;
            if (response.Is(out Response<IGetInstanceStringByIdErrorResultContract> errorResponse))
                return errorResponse.Message;
            throw new InvalidOperationException();
        }
    }
}