using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Requests.Behaviours;

namespace Nano35.Instance.Api.Requests
{
    public class GetAllClientTypesQuery : 
        IGetAllClientTypesRequestContract, 
        IQueryRequest<IGetAllClientTypesResultContract>
    {
        public Guid ClientTypeId { get; set; }
        public Guid ClientStateId { get; set; }
        public Guid InstanceId { get; set; }
        
        public class GetAllClientTypesHandler 
            : IRequestHandler<GetAllClientTypesQuery, IGetAllClientTypesResultContract>
        {
            private readonly IBus _bus;
            public GetAllClientTypesHandler(IBus bus)
            {
                _bus = bus;
            }

            public async Task<IGetAllClientTypesResultContract> Handle(
                GetAllClientTypesQuery message,
                CancellationToken cancellationToken)
            {
                var client = _bus.CreateRequestClient<IGetAllClientTypesRequestContract>(TimeSpan.FromSeconds(10));
                
                var response = await client
                    .GetResponse<IGetAllClientTypesSuccessResultContract, IGetAllClientTypesErrorResultContract>(message, cancellationToken);

                if (response.Is(out Response<IGetAllClientTypesSuccessResultContract> successResponse))
                    return successResponse.Message;
                
                if (response.Is(out Response<IGetAllClientTypesErrorResultContract> errorResponse))
                    return errorResponse.Message;
                
                throw new InvalidOperationException();
            }
        }
    }
}