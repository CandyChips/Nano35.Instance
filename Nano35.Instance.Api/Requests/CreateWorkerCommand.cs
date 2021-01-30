﻿using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Requests.Behaviours;

namespace Nano35.Instance.Api.Requests
{
    public class CreateWorkerHttpRequest
    {
        public Guid RoleId { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
    
    public class CreateWorkerCommand :
        ICreateWorkerRequestContract, 
        ICommandRequest<ICreateWorkerResultContract>
    {
        public Guid NewId { get; set; }
        public Guid InstanceId { get; set; }
        public Guid RoleId { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }

        public CreateWorkerCommand(
            Guid newId, 
            Guid instanceId,
            CreateWorkerHttpRequest request)
        {
            NewId = newId;
            InstanceId = instanceId;
            RoleId = request.RoleId;
            Name = request.Name;
            Comment = request.Comment;
            Phone = request.Phone;
            Email = request.Email;
            Password = request.Password;
            PasswordConfirm = request.PasswordConfirm;
        }
    
        public class CreateWorkerHandler : 
            IRequestHandler<CreateWorkerCommand, ICreateWorkerResultContract>
        {
            private readonly ILogger<CreateWorkerHandler> _logger;
            private readonly IBus _bus;
            public CreateWorkerHandler(
                IBus bus, 
                ILogger<CreateWorkerHandler> logger)
            {
                _bus = bus;
                _logger = logger;
            }
        
            public async Task<ICreateWorkerResultContract> Handle(
                CreateWorkerCommand message, 
                CancellationToken cancellationToken)
            {
                var client = _bus.CreateRequestClient<ICreateWorkerRequestContract>(TimeSpan.FromSeconds(10));
                var response = await client
                    .GetResponse<ICreateWorkerSuccessResultContract, ICreateWorkerErrorResultContract>(message, cancellationToken);
            
                if (response.Is(out Response<ICreateWorkerSuccessResultContract> successResponse))
                    return successResponse.Message;
            
                if (response.Is(out Response<ICreateWorkerErrorResultContract> errorResponse))
                    return errorResponse.Message;
            
                throw new InvalidOperationException();
            }
        }
    }
}