using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests;
using Nano35.Instance.Processor.Requests.CreateClient;
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
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<CreateUnitLogger>) _services.GetService(typeof(ILogger<CreateUnitLogger>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result = 
                await new CreateUnitLogger(logger,  
                    new CreateUnitValidator(
                        new CreateUnitTransaction(dbContext,
                            new CreateUnitRequest(dbContext)))
                ).Ask(message, context.CancellationToken);
            
            // Check response of create client request
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