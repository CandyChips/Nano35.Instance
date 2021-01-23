using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Services.Requests.Behaviours;

namespace Nano35.Instance.Api.Services.Requests
{
    public class CreateInstanceResultViewModel :
        ICreateInstanceSuccessResultContract,
        ICreateInstanceErrorResultContract
    {
        public string Error { get; set; }
        public Guid Id { get; set; }
    }
    
    public class CreateInstanceCommand :
        ICreateInstanceRequestContract, 
        ICommandRequest<CreateInstanceResultViewModel>
    {
        public Guid NewId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string RealName { get; set; }
        public string Email { get; set; }
        public string Info { get; set; }
        public string Phone { get; set; }
        public Guid TypeId { get; set; }
        public Guid RegionId { get; set; }
    }

    public class CreateInstanceCommandValidator : 
        AbstractValidator<CreateInstanceCommand>
    {
        public CreateInstanceCommandValidator()
        {
        }
    }
    
    public class CreateInstanceHandler : 
        IRequestHandler<CreateInstanceCommand, CreateInstanceResultViewModel>
    {
        private readonly ILogger<CreateInstanceHandler> _logger;
        private readonly IBus _bus;
        public CreateInstanceHandler(
            IBus bus, 
            ILogger<CreateInstanceHandler> logger)
        {
            _bus = bus;
            _logger = logger;
        }
        
        public async Task<CreateInstanceResultViewModel> Handle(
            CreateInstanceCommand message, 
            CancellationToken cancellationToken)
        {
            var result = new CreateInstanceResultViewModel();
            var client = _bus.CreateRequestClient<ICreateInstanceRequestContract>(TimeSpan.FromSeconds(10));
            var response = await client
                .GetResponse<ICreateInstanceSuccessResultContract, ICreateInstanceErrorResultContract>(message, cancellationToken);

            if (response.Is(out Response<ICreateInstanceSuccessResultContract> successResponse))
            {
                result.Id = successResponse.Message.Id;
                return result;
            }
            
            if (response.Is(out Response<ICreateInstanceErrorResultContract> errorResponse))
            {
                result.Error = errorResponse.Message.Error;
                return result;
            }
            
            throw new InvalidOperationException();
        }
    }
}