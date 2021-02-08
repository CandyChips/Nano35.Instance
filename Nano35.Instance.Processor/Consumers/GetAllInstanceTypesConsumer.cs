using System;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests;
using Nano35.Instance.Processor.Requests.GetAllInstanceTypes;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Consumers
{
    public class GetAllInstanceTypesConsumer : 
        IConsumer<IGetAllInstanceTypesRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public GetAllInstanceTypesConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllInstanceTypesRequestContract> context)
        {
            var dbcontect = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<GetAllInstanceTypesLogger>) _services.GetService(typeof(ILogger<GetAllInstanceTypesLogger>));
            
            var message = context.Message;
            
            var result =
                await new GetAllInstanceTypesLogger(logger,
                    new GetAllInstanceTypesValidator(
                        new GetAllInstanceTypesRequest(dbcontect))
                ).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IGetAllInstanceTypesSuccessResultContract:
                    await context.RespondAsync<IGetAllInstanceTypesSuccessResultContract>(result);
                    break;
                case IGetAllInstanceTypesErrorResultContract:
                    await context.RespondAsync<IGetAllInstanceTypesErrorResultContract>(result);
                    break;
            }
        }
    }
}