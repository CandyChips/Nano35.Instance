using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.GetAllClientStates
{
    public class GetAllClientStatesRequest :
        IPipelineNode<IGetAllClientStatesRequestContract, IGetAllClientStatesResultContract>
    {
        private readonly IBus _bus;

        public GetAllClientStatesRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IGetAllClientStatesResultContract> Ask(IGetAllClientStatesRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetAllClientStatesRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetAllClientStatesSuccessResultContract, IGetAllClientStatesErrorResultContract>(input);
            
            if (response.Is(out Response<IGetAllClientStatesSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetAllClientStatesErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}