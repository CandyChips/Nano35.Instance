using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllInstanceTypes
{
    public class GetAllInstanceTypesRequest :
        IPipelineNode<
            IGetAllInstanceTypesRequestContract,
            IGetAllInstanceTypesResultContract>
    {
        private readonly IBus _bus;

        public GetAllInstanceTypesRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IGetAllInstanceTypesResultContract> Ask(
            IGetAllInstanceTypesRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetAllInstanceTypesRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetAllInstanceTypesSuccessResultContract, IGetAllInstanceTypesErrorResultContract>(input);
            
            if (response.Is(out Response<IGetAllInstanceTypesSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetAllInstanceTypesErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}