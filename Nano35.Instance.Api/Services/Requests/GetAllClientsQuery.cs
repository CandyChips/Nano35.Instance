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
    public class GetAllClientsQuery : 
        IGetAllClientsRequestContract, 
        IQueryRequest<IGetAllClientsResultContract>
    {
        public Guid ClientTypeId { get; set; }
        public Guid ClientStateId { get; set; }
        public Guid InstanceId { get; set; }
        
        public class GetAllClientsHandler 
            : IRequestHandler<GetAllClientsQuery, IGetAllClientsResultContract>
        {
            private readonly IBus _bus;
            public GetAllClientsHandler(IBus bus)
            {
                _bus = bus;
            }

            public async Task<IGetAllClientsResultContract> Handle(
                GetAllClientsQuery message,
                CancellationToken cancellationToken)
            {
                var client = _bus.CreateRequestClient<IGetAllClientsRequestContract>(TimeSpan.FromSeconds(10));
                var response = await client
                    .GetResponse<IGetAllClientsSuccessResultContract, IGetAllClientsErrorResultContract>(message, cancellationToken);

                if (response.Is(out Response<IGetAllClientsSuccessResultContract> responseA))
                {
                    return responseA.Message;
                }
                else if (response.Is(out Response<IGetAllClientsErrorResultContract> responseB))
                {
                    return responseB.Message;
                }
                throw new InvalidOperationException();
            }
        }
    }
}