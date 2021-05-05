using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateUnitsType
{
    public class UpdateUnitsTypeConsumer : IConsumer<IUpdateUnitsTypeRequestContract>
    {
        private readonly IServiceProvider  _services;
        public UpdateUnitsTypeConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IUpdateUnitsTypeRequestContract> context)
        {
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var result = 
                await new LoggedUseCasePipeNode<IUpdateUnitsTypeRequestContract, IUpdateUnitsTypeResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateUnitsTypeRequestContract>)) as ILogger<IUpdateUnitsTypeRequestContract>,
                    new TransactedUseCasePipeNode<IUpdateUnitsTypeRequestContract, IUpdateUnitsTypeResultContract>(dbContext,
                        new UpdateUnitsTypeUseCase(dbContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}