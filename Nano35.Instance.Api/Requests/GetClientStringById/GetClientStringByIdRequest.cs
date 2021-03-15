using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetClientStringById
{
    public class GetClientStringByIdRequest :
        IPipelineNode<IGetClientStringByIdRequestContract, IGetClientStringByIdResultContract>
    {
        private readonly IBus _bus;

        public GetClientStringByIdRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IGetClientStringByIdResultContract> Ask(IGetClientStringByIdRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetClientStringByIdRequestContract>(TimeSpan.FromSeconds(10));
            var response = await client
                .GetResponse<IGetClientStringByIdSuccessResultContract, IGetClientStringByIdErrorResultContract>(input);
            if (response.Is(out Response<IGetClientStringByIdSuccessResultContract> successResponse))
                return successResponse.Message;
            if (response.Is(out Response<IGetClientStringByIdErrorResultContract> errorResponse))
                return errorResponse.Message;
            throw new InvalidOperationException();
        }
    }
}