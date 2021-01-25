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
            private readonly ILogger<GetAllInstancesHandler> _logger;
            private readonly IBus _bus;
            public GetAllInstancesHandler(
                IBus bus, 
                ILogger<GetAllInstancesHandler> logger)
            {
                _bus = bus;
                _logger = logger;
            }

            public async Task<IGetAllInstancesResultContract> Handle(
                GetAllInstancesQuery message,
                CancellationToken cancellationToken)
            {
                var client = _bus.CreateRequestClient<IGetAllInstancesRequestContract>(TimeSpan.FromSeconds(10));
                var response = await client
                    .GetResponse<IGetAllInstancesSuccessResultContract, IGetAllInstancesErrorResultContract>(message, cancellationToken);

                if (response.Is(out Response<IGetAllInstancesSuccessResultContract> responseA))
                {
                    return responseA.Message;
                }
                else if (response.Is(out Response<IGetAllInstancesErrorResultContract> responseB))
                {
                    throw new Exception();
                }
                throw new InvalidOperationException();
            }
        }

        public class GetAllInstancesQueryExceptionHandler :
            RequestExceptionHandler<GetAllInstancesHandler, IGetAllInstancesResultContract, Exception>
        {
            protected override void Handle(
                GetAllInstancesHandler request, 
                Exception exception, 
                RequestExceptionHandlerState<IGetAllInstancesResultContract> state)
            {
                //state.Handled()
            }
        }
    }
}