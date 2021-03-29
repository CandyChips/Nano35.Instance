using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.CreateWorker
{
    public class CreateWorkerConsumer : 
        IConsumer<ICreateWorkerRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public CreateWorkerConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<ICreateWorkerRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<LoggedCreateWorkerRequest>) _services.GetService(typeof(ILogger<LoggedCreateWorkerRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result = 
                await new LoggedCreateWorkerRequest(logger,  
                    new ValidatedCreateWorkerRequest(
                        new TransactedCreateWorkerRequest(dbContext,
                            new CreateWorkerUseCase(bus, dbContext)))).Ask(message, context.CancellationToken);
            
            // Check response of create client request
            switch (result)
            {
                case ICreateWorkerSuccessResultContract:
                    await context.RespondAsync<ICreateWorkerSuccessResultContract>(result);
                    break;
                case ICreateWorkerErrorResultContract:
                    await context.RespondAsync<ICreateWorkerErrorResultContract>(result);
                    break;
            }
        }
    }
}