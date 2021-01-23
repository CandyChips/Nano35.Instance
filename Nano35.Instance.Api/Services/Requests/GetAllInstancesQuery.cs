using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Api.Services.Requests.Behaviours;

namespace Nano35.Instance.Api.Services.Requests
{
    public class GetAllInstancesQuery : 
        IGetAllInstancesRequestContract, 
        IQueryRequest<IGetAllInstancesSuccessResultContract>
    {
        public Guid UserId { get; set; }
        public Guid InstanceTypeId { get; set; }
        public Guid RegionId { get; set; }
    }

    public class GetAllInstancesHandler 
        : IRequestHandler<GetAllInstancesQuery, IGetAllInstancesSuccessResultContract>
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

        public async Task<IGetAllInstancesSuccessResultContract> Handle(
            GetAllInstancesQuery message,
            CancellationToken cancellationToken)
        {
            var client = _bus.CreateRequestClient<IGetAllInstancesRequestContract>(TimeSpan.FromSeconds(10));
            var response = await client
                .GetResponse<IGetAllInstancesSuccessResultContract>(message, cancellationToken);

            return response.Message;
            
            throw new InvalidOperationException();
        }
    }
}