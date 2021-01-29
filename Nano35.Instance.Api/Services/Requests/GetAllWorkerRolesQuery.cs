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
    public class GetAllWorkerRolesQuery : 
        IGetAllWorkerRolesRequestContract, 
        IQueryRequest<IGetAllWorkerRolesResultContract>
    {
        public class GetAllWorkerRolesHandler 
            : IRequestHandler<GetAllWorkerRolesQuery, IGetAllWorkerRolesResultContract>
        {
            private readonly ILogger<GetAllWorkerRolesHandler> _logger;
            private readonly IBus _bus;
            public GetAllWorkerRolesHandler(
                IBus bus, 
                ILogger<GetAllWorkerRolesHandler> logger)
            {
                _bus = bus;
                _logger = logger;
            }

            public async Task<IGetAllWorkerRolesResultContract> Handle(
                GetAllWorkerRolesQuery message,
                CancellationToken cancellationToken)
            {
                var client = _bus.CreateRequestClient<IGetAllWorkerRolesRequestContract>(TimeSpan.FromSeconds(10));
                
                var response = await client
                    .GetResponse<IGetAllWorkerRolesSuccessResultContract, IGetAllWorkerRolesErrorResultContract>(message, cancellationToken);

                if (response.Is(out Response<IGetAllWorkerRolesSuccessResultContract> successResponse))
                {
                    return successResponse.Message;
                }
                
                if (response.Is(out Response<IGetAllWorkerRolesErrorResultContract> errorResponse))
                {
                    return errorResponse.Message;
                }
                
                throw new InvalidOperationException();
            }
        }
    }
}