using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsType
{
    public class UpdateClientsTypeConsumer : IConsumer<IUpdateClientsTypeRequestContract>
    {
        private readonly IServiceProvider  _services;
        public UpdateClientsTypeConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IUpdateClientsTypeRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var result = 
                await new LoggedPipeNode<IUpdateClientsTypeRequestContract, IUpdateClientsTypeResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateClientsTypeRequestContract>)) as ILogger<IUpdateClientsTypeRequestContract>,
                    new TransactedPipeNode<IUpdateClientsTypeRequestContract, IUpdateClientsTypeResultContract>(dbContext,
                        new UpdateClientsType(dbContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}