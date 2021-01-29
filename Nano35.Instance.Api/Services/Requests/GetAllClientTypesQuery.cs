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

                if (response.Is(out Response<IGetAllClientTypesSuccessResultContract> responseA))
                {
                    return responseA.Message;
                }
                else if (response.Is(out Response<IGetAllClientTypesErrorResultContract> responseB))
                {
                    return responseB.Message;
                }
                throw new InvalidOperationException();
            }
        }
    }
}