using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllWorkers
{
    public class GetAllWorkersRequest :
        IPipelineNode<IGetAllWorkersRequestContract, IGetAllWorkersResultContract>
    {
        private readonly IBus _bus;

        public GetAllWorkersRequest(IBus bus) { _bus = bus; }
        
        public async Task<IGetAllWorkersResultContract> Ask(IGetAllWorkersRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetAllWorkersRequestContract>(TimeSpan.FromSeconds(10));
            var response = await client
                .GetResponse<IGetAllWorkersSuccessResultContract, IGetAllWorkersErrorResultContract>(input);
            if (response.Is(out Response<IGetAllWorkersSuccessResultContract> successResponse))
                return successResponse.Message;
            if (response.Is(out Response<IGetAllWorkersErrorResultContract> errorResponse))
                return errorResponse.Message;
            throw new InvalidOperationException();
        }
    }
}