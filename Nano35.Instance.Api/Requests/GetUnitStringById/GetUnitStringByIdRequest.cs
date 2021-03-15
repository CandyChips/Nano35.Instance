using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetUnitStringById
{
    public class GetUnitStringByIdRequest :
        IPipelineNode<IGetUnitStringByIdRequestContract, IGetUnitStringByIdResultContract>
    {
        private readonly IBus _bus;

        public GetUnitStringByIdRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IGetUnitStringByIdResultContract> Ask(IGetUnitStringByIdRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetUnitStringByIdRequestContract>(TimeSpan.FromSeconds(10));
            var response = await client
                .GetResponse<IGetUnitStringByIdSuccessResultContract, IGetUnitStringByIdErrorResultContract>(input);
            if (response.Is(out Response<IGetUnitStringByIdSuccessResultContract> successResponse))
                return successResponse.Message;
            if (response.Is(out Response<IGetUnitStringByIdErrorResultContract> errorResponse))
                return errorResponse.Message;
            throw new InvalidOperationException();
        }
    }
}