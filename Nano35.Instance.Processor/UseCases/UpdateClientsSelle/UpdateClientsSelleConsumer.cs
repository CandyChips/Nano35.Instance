using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsSelle
{
    public class UpdateClientsSelleConsumer : IConsumer<IUpdateClientsSelleRequestContract>
    {
        private readonly IServiceProvider  _services;
        public UpdateClientsSelleConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IUpdateClientsSelleRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var result = 
                await new LoggedPipeNode<IUpdateClientsSelleRequestContract, IUpdateClientsSelleResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateClientsSelleRequestContract>)) as ILogger<IUpdateClientsSelleRequestContract>,
                    new TransactedPipeNode<IUpdateClientsSelleRequestContract, IUpdateClientsSelleResultContract>(dbContext,
                        new UpdateClientsSelle(dbContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}