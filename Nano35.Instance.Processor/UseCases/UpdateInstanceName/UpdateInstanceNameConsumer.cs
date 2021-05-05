using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateInstanceName
{
    public class UpdateInstanceNameConsumer : IConsumer<IUpdateInstanceNameRequestContract>
    {
        private readonly IServiceProvider  _services;
        public UpdateInstanceNameConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IUpdateInstanceNameRequestContract> context)
        {
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var result =
                await new LoggedUseCasePipeNode<IUpdateInstanceNameRequestContract, IUpdateInstanceNameResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateInstanceNameRequestContract>)) as ILogger<IUpdateInstanceNameRequestContract>,
                    new TransactedUseCasePipeNode<IUpdateInstanceNameRequestContract, IUpdateInstanceNameResultContract>(dbContext,
                        new UpdateInstanceNameUseCase(dbContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}