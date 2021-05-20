using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateUnitsWorkingFormat
{
    public class UpdateUnitsWorkingFormatConsumer : IConsumer<IUpdateUnitsWorkingFormatRequestContract>
    {
        private readonly IServiceProvider  _services;
        public UpdateUnitsWorkingFormatConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IUpdateUnitsWorkingFormatRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var result = 
                await new LoggedPipeNode<IUpdateUnitsWorkingFormatRequestContract, IUpdateUnitsWorkingFormatResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateUnitsWorkingFormatRequestContract>)) as ILogger<IUpdateUnitsWorkingFormatRequestContract>,
                    new TransactedPipeNode<IUpdateUnitsWorkingFormatRequestContract, IUpdateUnitsWorkingFormatResultContract>(dbContext,
                        new UpdateUnitsWorkingFormat(dbContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}