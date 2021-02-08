using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests;
using Nano35.Instance.Processor.Requests.CreateInstance;
using Nano35.Instance.Processor.Requests.CreateUnit;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Consumers
{
    public class CreateUnitConsumer : 
        IConsumer<ICreateUnitRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public CreateUnitConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<ICreateUnitRequestContract> context)
        {
            var dbcontect = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<CreateUnitLogger>)_services.GetService(typeof(ILogger<CreateUnitLogger>));
            
            var message = context.Message;
            
            var result =
                await new CreateUnitLogger(logger,
                    new CreateUnitValidator(
                        new CreateUnitTransaction(
                            new CreateUnitRequest(dbcontect),dbcontect))
                ).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case ICreateUnitSuccessResultContract:
                    await context.RespondAsync<ICreateUnitSuccessResultContract>(result);
                    break;
                case ICreateUnitErrorResultContract:
                    await context.RespondAsync<ICreateUnitErrorResultContract>(result);
                    break;
            }
        }
    }
}