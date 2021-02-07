using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;

namespace Nano35.Instance.Api.Requests.GetAllClients
{
    public class GetAllClientsRequest :
        IPipelineNode<IGetAllClientsRequestContract, IGetAllClientsResultContract>
    {
        private readonly IBus _bus;

        public GetAllClientsRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IGetAllClientsResultContract> Ask(IGetAllClientsRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetAllClientsRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetAllClientsSuccessResultContract, IGetAllClientsErrorResultContract>(input);
            
            if (response.Is(out Response<IGetAllClientsSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetAllClientsErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}
