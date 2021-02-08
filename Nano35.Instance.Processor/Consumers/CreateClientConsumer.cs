using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests.CreateClient;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Consumers
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
            var dbcontect = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<CreateClientLogger>) _services.GetService(typeof(ILogger<CreateClientLogger>));
            
            var message = context.Message;
            
            var result =
                await new CreateClientLogger(logger,
                    new CreateClientValidator(
                        new CreateClientTransaction(dbcontect,
                            new CreateClientRequest(dbcontect)))
                ).Ask(message, context.CancellationToken);

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