using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateUnitsName
{
    public class UpdateUnitsNameConsumer : IConsumer<IUpdateUnitsNameRequestContract>
    {
        private readonly IServiceProvider  _services;
        public UpdateUnitsNameConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IUpdateUnitsNameRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var result = 
                await new LoggedPipeNode<IUpdateUnitsNameRequestContract, IUpdateUnitsNameResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateUnitsNameRequestContract>)) as ILogger<IUpdateUnitsNameRequestContract>,
                    new TransactedPipeNode<IUpdateUnitsNameRequestContract, IUpdateUnitsNameResultContract>(dbContext, 
                        new UpdateUnitsName(dbContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}