using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsName
{
    public class UpdateClientsNameConsumer : IConsumer<IUpdateClientsNameRequestContract>
    {
        private readonly IServiceProvider  _services;
        public UpdateClientsNameConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IUpdateClientsNameRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var result = 
                await new LoggedUseCasePipeNode<IUpdateClientsNameRequestContract, IUpdateClientsNameResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateClientsNameRequestContract>)) as ILogger<IUpdateClientsNameRequestContract>,
                    new TransactedUseCasePipeNode<IUpdateClientsNameRequestContract, IUpdateClientsNameResultContract>(dbContext,
                        new UpdateClientsNameUseCase(dbContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}