using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.CreateClient
{
    public class CreateClientConsumer : 
        IConsumer<ICreateClientRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public CreateClientConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        public async Task Consume(
            ConsumeContext<ICreateClientRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedCreateClientRequest>) _services.GetService(typeof(ILogger<LoggedCreateClientRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result = 
                await new LoggedCreateClientRequest(logger,  
                    new ValidatedCreateClientRequest(
                        new TransactedCreateClientRequest(dbContext,
                            new CreateClientRequest(dbContext)))
                    ).Ask(message, context.CancellationToken);
            
            // Check response of create client request
            switch (result)
            {
                case ICreateClientSuccessResultContract:
                    await context.RespondAsync<ICreateClientSuccessResultContract>(result);
                    break;
                case ICreateClientErrorResultContract:
                    await context.RespondAsync<ICreateClientErrorResultContract>(result);
                    break;
            }
        }
    }
}