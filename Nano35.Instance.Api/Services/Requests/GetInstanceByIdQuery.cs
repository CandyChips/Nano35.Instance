﻿using System;
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
    public class GetInstanceByIdQuery : 
        IGetInstanceByIdRequestContract, 
        IQueryRequest<IGetInstanceByIdResultContract>
    {
        public Guid InstanceId { get; set; }

        public GetInstanceByIdQuery(Guid id)
        {
            InstanceId = id;
        }
        
        public class GetInstanceByIdHandler 
            : IRequestHandler<GetInstanceByIdQuery, IGetInstanceByIdResultContract>
        {
            private readonly ILogger<GetInstanceByIdHandler> _logger;
            private readonly IBus _bus;
            public GetInstanceByIdHandler(
                IBus bus, 
                ILogger<GetInstanceByIdHandler> logger)
            {
                _bus = bus;
                _logger = logger;
            }

            public async Task<IGetInstanceByIdResultContract> Handle(
                GetInstanceByIdQuery message,
                CancellationToken cancellationToken)
            {
                var client = _bus.CreateRequestClient<IGetInstanceByIdRequestContract>(TimeSpan.FromSeconds(10));
                var response = await client
                    .GetResponse<IGetInstanceByIdSuccessResultContract, IGetInstanceByIdErrorResultContract>(message, cancellationToken);

                if (response.Is(out Response<IGetInstanceByIdSuccessResultContract> responseA))
                {
                    return responseA.Message;
                }
                else if (response.Is(out Response<IGetInstanceByIdErrorResultContract> responseB))
                {
                    throw new Exception();
                }
                throw new InvalidOperationException();
            }
        }
    }
}