using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Requests.Behaviours;

namespace Nano35.Instance.Api.Requests
{
    public class GetAllWorkersQuery : 
        IGetAllWorkersRequestContract, 
        IQueryRequest<IGetAllWorkersResultContract>
    {

        public Guid InstanceId { get; set; }
        public Guid WorkersRoleId { get; set; }

        public class GetAllWorkersHandler 
            : IRequestHandler<GetAllWorkersQuery, IGetAllWorkersResultContract>
        {
            private readonly IBus _bus;
            public GetAllWorkersHandler(IBus bus)
            {
                _bus = bus;
            }

            public async Task<IGetAllWorkersResultContract> Handle(
                GetAllWorkersQuery message,
                CancellationToken cancellationToken)
            {
                var client = _bus.CreateRequestClient<IGetAllWorkersRequestContract>(TimeSpan.FromSeconds(10));
                
                var response = await client
                    .GetResponse<IGetAllWorkersSuccessResultContract, IGetAllWorkersErrorResultContract>(message, cancellationToken);

                if (response.Is(out Response<IGetAllWorkersSuccessResultContract> successResponse))
                    return successResponse.Message;
                
                if (response.Is(out Response<IGetAllWorkersErrorResultContract> errorResponse))
                    return errorResponse.Message;
                
                throw new InvalidOperationException();
            }
        }
    }
}