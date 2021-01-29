using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Api.Services.Requests.Behaviours;

namespace Nano35.Instance.Api.Services.Requests
{
    public class GetAllInstancesQuery : 
        IGetAllInstancesRequestContract, 
        IQueryRequest<IGetAllInstancesResultContract>
    {
        public Guid UserId { get; set; }
        public Guid InstanceTypeId { get; set; }
        public Guid RegionId { get; set; }

        public class GetAllInstancesHandler 
            : IRequestHandler<GetAllInstancesQuery, IGetAllInstancesResultContract>
        {
            private readonly IBus _bus;
            public GetAllInstancesHandler(IBus bus)
            {
                _bus = bus;
            }

            public async Task<IGetAllInstancesResultContract> Handle(
                GetAllInstancesQuery message,
                CancellationToken cancellationToken)
            {
                var client = _bus.CreateRequestClient<IGetAllInstancesRequestContract>(TimeSpan.FromSeconds(10));
                
                var response = await client
                    .GetResponse<IGetAllInstancesSuccessResultContract, IGetAllInstancesErrorResultContract>(message, cancellationToken);

                if (response.Is(out Response<IGetAllInstancesSuccessResultContract> successResponse))
                    return successResponse.Message;
                
                if (response.Is(out Response<IGetAllInstancesErrorResultContract> errorResponse))
                    return errorResponse.Message;
                
                throw new InvalidOperationException();
            }
        }
    }
}