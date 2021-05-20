using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.CreateWorker
{
    public class CreateWorkerConsumer : IConsumer<ICreateWorkerRequestContract>
    {
        private readonly IServiceProvider  _services;
        public CreateWorkerConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<ICreateWorkerRequestContract> context)
        {
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var result = 
                await new LoggedPipeNode<ICreateWorkerRequestContract, ICreateWorkerResultContract>(
                        _services.GetService(typeof(ILogger<ICreateWorkerRequestContract>)) as ILogger<ICreateWorkerRequestContract>,  
                        new TransactedPipeNode<ICreateWorkerRequestContract, ICreateWorkerResultContract>(
                            dbContext,
                            new CreateWorker(
                                _services.GetService(typeof(IBus)) as IBus, 
                                dbContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}