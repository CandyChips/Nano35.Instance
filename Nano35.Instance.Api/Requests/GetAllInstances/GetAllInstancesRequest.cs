using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllInstances
{
    public class GetAllInstancesRequest :
        IPipelineNode<IGetAllInstancesRequestContract, IGetAllInstancesResultContract>
    {
        private readonly IBus _bus;

        public GetAllInstancesRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IGetAllInstancesResultContract> Ask(IGetAllInstancesRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetAllInstancesRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetAllInstancesSuccessResultContract, IGetAllInstancesErrorResultContract>(input);
            
            if (response.Is(out Response<IGetAllInstancesSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetAllInstancesErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}