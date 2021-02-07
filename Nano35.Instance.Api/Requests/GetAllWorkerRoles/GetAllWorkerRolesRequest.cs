using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllWorkerRoles
{
    public class GetAllWorkerRolesRequest :
        IPipelineNode<IGetAllWorkerRolesRequestContract, IGetAllWorkerRolesResultContract>
    {
        private readonly IBus _bus;

        public GetAllWorkerRolesRequest(
            IBus bus)
        {
            _bus = bus;
        }
        
        public async Task<IGetAllWorkerRolesResultContract> Ask(IGetAllWorkerRolesRequestContract input)
        {
            var client = _bus.CreateRequestClient<IGetAllWorkerRolesRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<IGetAllWorkerRolesSuccessResultContract, IGetAllWorkerRolesErrorResultContract>(input);
            
            if (response.Is(out Response<IGetAllWorkerRolesSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<IGetAllWorkerRolesErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}