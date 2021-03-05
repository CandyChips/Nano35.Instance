using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllUnits
{
    public class GetAllUnitsRequest :
        IPipelineNode<
            IGetAllUnitsRequestContract, 
            IGetAllUnitsResultContract>
    {
        private readonly IBus _bus;

        public GetAllUnitsRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IGetAllUnitsResultContract> Ask(
            IGetAllUnitsRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetAllUnitsRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetAllUnitsSuccessResultContract, IGetAllUnitsErrorResultContract>(input);
            
            if (response.Is(out Response<IGetAllUnitsSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetAllUnitsErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}