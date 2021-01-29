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
    public class GetAllRegionsQuery : 
        IGetAllRegionsRequestContract, 
        IQueryRequest<IGetAllRegionsResultContract>
    {
        public class GetAllRegionsHandler 
            : IRequestHandler<GetAllRegionsQuery, IGetAllRegionsResultContract>
        {
            private readonly ILogger<GetAllRegionsHandler> _logger;
            private readonly IBus _bus;
            public GetAllRegionsHandler(
                IBus bus, 
                ILogger<GetAllRegionsHandler> logger)
            {
                _bus = bus;
                _logger = logger;
            }

            public async Task<IGetAllRegionsResultContract> Handle(
                GetAllRegionsQuery message,
                CancellationToken cancellationToken)
            {
                var client = _bus.CreateRequestClient<IGetAllRegionsRequestContract>(TimeSpan.FromSeconds(10));
                var response = await client
                    .GetResponse<IGetAllRegionsSuccessResultContract, IGetAllRegionsErrorResultContract>(message, cancellationToken);

                if (response.Is(out Response<IGetAllRegionsSuccessResultContract> successResponse))
                {
                    return successResponse.Message;
                }
            
                if (response.Is(out Response<IGetAllRegionsErrorResultContract> errorResponse))
                {
                    return errorResponse.Message;
                }
            
                throw new InvalidOperationException();
            }
        }
    }

}