using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateWorkersName
{
    public class UpdateWorkersNameConsumer : IConsumer<IUpdateWorkersNameRequestContract>
    {
        private readonly IServiceProvider  _services;
        public UpdateWorkersNameConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IUpdateWorkersNameRequestContract> context)
        {
            var dbContext = _services.GetService(typeof(ApplicationContext)) as ApplicationContext;
            var result = 
                await new LoggedUseCasePipeNode<IUpdateWorkersNameRequestContract, IUpdateWorkersNameResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateWorkersNameRequestContract>)) as ILogger<IUpdateWorkersNameRequestContract>,
                        new TransactedUseCasePipeNode<IUpdateWorkersNameRequestContract, IUpdateWorkersNameResultContract>(dbContext,
                            new UpdateWorkersNameUseCase(dbContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}