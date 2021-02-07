using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllUnitTypes
{
    public class GetAllUnitTypesRequest :
        IPipelineNode<IGetAllUnitTypesRequestContract, IGetAllUnitTypesResultContract>
    {
        private readonly IBus _bus;

        public GetAllUnitTypesRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IGetAllUnitTypesResultContract> Ask(IGetAllUnitTypesRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetAllUnitTypesRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetAllUnitTypesSuccessResultContract, IGetAllUnitTypesErrorResultContract>(input);
            
            if (response.Is(out Response<IGetAllUnitTypesSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetAllUnitTypesErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}