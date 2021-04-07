using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.CreateInstance
{
    public class CreateInstanceConsumer : 
        IConsumer<ICreateInstanceRequestContract>
    {
        private readonly IServiceProvider  _services;
        public CreateInstanceConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        public async Task Consume(ConsumeContext<ICreateInstanceRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<ICreateInstanceRequestContract>) _services.GetService(typeof(ILogger<ICreateInstanceRequestContract>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result = 
                await new LoggedPipeNode<ICreateInstanceRequestContract, ICreateInstanceResultContract>(logger,
                    new ValidatedCreateInstanceRequest(
                        new TransactedPipeNode<ICreateInstanceRequestContract, ICreateInstanceResultContract>(dbContext,
                            new CreateInstanceUseCase(dbContext)))).Ask(message, context.CancellationToken);
            
            // Check response of create instance request
            switch (result)
            {
                case ICreateInstanceSuccessResultContract:
                    await context.RespondAsync<ICreateInstanceSuccessResultContract>(result);
                    break;
                case ICreateInstanceErrorResultContract:
                    await context.RespondAsync<ICreateInstanceErrorResultContract>(result);
                    break;
            }
        }
    }
}