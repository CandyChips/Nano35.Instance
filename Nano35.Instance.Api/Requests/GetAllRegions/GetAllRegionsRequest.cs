using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllRegions
{
    public class GetAllRegionsRequest :
        IPipelineNode<IGetAllRegionsRequestContract, IGetAllRegionsResultContract>
    {
        private readonly IBus _bus;

        public GetAllRegionsRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IGetAllRegionsResultContract> Ask(IGetAllRegionsRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetAllRegionsRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetAllRegionsSuccessResultContract, IGetAllRegionsErrorResultContract>(input);
            
            if (response.Is(out Response<IGetAllRegionsSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetAllRegionsErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}