using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Services.Helpers;
using Nano35.Instance.Api.Services.Requests.Behaviours;

namespace Nano35.Instance.Api.Services.Requests
{
    public class CreateClientCommand :
        ICreateClientRequestContract, 
        ICommandRequest<ICreateClientResultContract>
    {
        public Guid NewId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public float Salle { get; set; }
        public Guid ClientTypeId { get; set; }
        public Guid ClientStateId { get; set; }
        public Guid UserId { get; set; }
        public Guid InstanceId { get; set; }
    }
    
    public class CreateClientHandler : 
        IRequestHandler<CreateClientCommand, ICreateClientResultContract>
    {
        private readonly ILogger<CreateClientHandler> _logger;
        private readonly ICustomAuthStateProvider _auth;
        private readonly IBus _bus;
        public CreateClientHandler(
            IBus bus, 
            ILogger<CreateClientHandler> logger, 
            ICustomAuthStateProvider auth)
        {
            _bus = bus;
            _logger = logger;
            _auth = auth;
        }
        
        public async Task<ICreateClientResultContract> Handle(
            CreateClientCommand message, 
            CancellationToken cancellationToken)
        {
            message.UserId = _auth.CurrentUserId;
            
            var client = _bus.CreateRequestClient<ICreateClientRequestContract>(TimeSpan.FromSeconds(10));
            
            var response = await client
                .GetResponse<ICreateClientSuccessResultContract, ICreateClientErrorResultContract>(message, cancellationToken);
            
            if (response.Is(out Response<ICreateClientSuccessResultContract> successResponse))
                return successResponse.Message;
            
            if (response.Is(out Response<ICreateClientErrorResultContract> errorResponse))
                return errorResponse.Message;
            
            throw new InvalidOperationException();
        }
    }
}