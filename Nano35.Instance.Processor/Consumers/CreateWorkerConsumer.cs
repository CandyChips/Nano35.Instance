using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
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
            var dbcontect = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<CreateWorkerLogger>) _services.GetService(typeof(ILogger<CreateWorkerLogger>));
            
            var message = context.Message;
            
            var result =
                await new CreateWorkerLogger(logger,
                    new CreateWorkerValidator(
                        new CreateWorkerTransaction(dbcontect,
                            new CreateWorkerRequest(bus, dbcontect)))
                ).Ask(message, context.CancellationToken);
            
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