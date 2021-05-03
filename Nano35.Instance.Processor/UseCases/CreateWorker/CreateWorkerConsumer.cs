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
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var bus = (IBus) _services.GetService(typeof(IBus));
            var logger = (ILogger<ICreateWorkerRequestContract>) _services.GetService(typeof(ILogger<ICreateWorkerRequestContract>));
            var message = context.Message;
            var result = 
                await new LoggedUseCasePipeNode<ICreateWorkerRequestContract, ICreateWorkerSuccessResultContract>(logger,  
                        new TransactedUseCasePipeNode<ICreateWorkerRequestContract, ICreateWorkerSuccessResultContract>(dbContext,
                            new CreateWorkerUseCase(bus, dbContext)))
                    .Ask(message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}