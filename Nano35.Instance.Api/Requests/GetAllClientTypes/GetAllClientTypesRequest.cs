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

        public GetAllClientTypesRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IGetAllClientTypesResultContract> Ask(IGetAllClientTypesRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetAllClientTypesRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetAllClientTypesSuccessResultContract, IGetAllClientTypesErrorResultContract>(input);
            
            if (response.Is(out Response<IGetAllClientTypesSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetAllClientTypesErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}