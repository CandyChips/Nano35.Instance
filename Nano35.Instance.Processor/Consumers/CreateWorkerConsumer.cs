using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests;
using Nano35.Instance.Processor.Requests.CreateUnit;
using Nano35.Instance.Processor.Requests.CreateWorker;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Consumers
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
            var logger = (ILogger<CreateWorkerLogger>) _services.GetService(typeof(ILogger<CreateWorkerLogger>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result = 
                await new CreateWorkerLogger(logger,  
                    new CreateWorkerValidator(
                        new CreateWorkerTransaction(dbContext,
                            new CreateWorkerRequest(bus, dbContext)))
                    ).Ask(message, context.CancellationToken);
            
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