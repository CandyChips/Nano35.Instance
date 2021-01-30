﻿using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;
using Nano35.Instance.Api.Requests.Behaviours;

namespace Nano35.Instance.Api.Requests
{
    public class CreateUnitCommand :
        ICreateUnitRequestContract, 
        ICommandRequest<ICreateUnitResultContract>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string WorkingFormat { get; set; }
        public string Phone { get; set; }
        public Guid UnitTypeId { get; set; }
        public Guid CreatorId { get; set; }
        public Guid InstanceId { get; set; }
    
        public class CreateUnitHandler : 
            IRequestHandler<CreateUnitCommand, ICreateUnitResultContract>
        {
            private readonly ILogger<CreateUnitHandler> _logger;
            private readonly ICustomAuthStateProvider _auth;
            private readonly IBus _bus;
            public CreateUnitHandler(
                IBus bus, 
                ILogger<CreateUnitHandler> logger, 
                ICustomAuthStateProvider auth)
            {
                _bus = bus;
                _logger = logger;
                _auth = auth;
            }
        
            public async Task<ICreateUnitResultContract> Handle(
                CreateUnitCommand message, 
                CancellationToken cancellationToken)
            {
                message.CreatorId = _auth.CurrentUserId;
            
                var client = _bus.CreateRequestClient<ICreateUnitRequestContract>(TimeSpan.FromSeconds(10));
            
                var response = await client
                    .GetResponse<ICreateUnitSuccessResultContract, ICreateUnitErrorResultContract>(message, cancellationToken);
            
                if (response.Is(out Response<ICreateUnitSuccessResultContract> successResponse))
                    return successResponse.Message;
            
                if (response.Is(out Response<ICreateUnitErrorResultContract> errorResponse))
                    return errorResponse.Message;
            
                throw new InvalidOperationException();
            }
        }
    }
}