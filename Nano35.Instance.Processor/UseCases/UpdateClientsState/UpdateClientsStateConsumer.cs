using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsState
{
    public class UpdateClientsStateConsumer : IConsumer<IUpdateClientsStateRequestContract>
    {
        private readonly IServiceProvider  _services;
        public UpdateClientsStateConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IUpdateClientsStateRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var result = 
                await new LoggedUseCasePipeNode<IUpdateClientsStateRequestContract, IUpdateClientsStateResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateClientsStateRequestContract>)) as ILogger<IUpdateClientsStateRequestContract>,
                    new TransactedUseCasePipeNode<IUpdateClientsStateRequestContract, IUpdateClientsStateResultContract>(dbContext,
                        new UpdateClientsStateUseCase(dbContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}