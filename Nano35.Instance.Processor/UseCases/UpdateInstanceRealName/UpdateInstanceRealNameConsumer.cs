using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateInstanceRealName
{
    public class UpdateInstanceRealNameConsumer : IConsumer<IUpdateInstanceRealNameRequestContract>
    {
        private readonly IServiceProvider  _services;
        public UpdateInstanceRealNameConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IUpdateInstanceRealNameRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var result = 
                await new LoggedUseCasePipeNode<IUpdateInstanceRealNameRequestContract, IUpdateInstanceRealNameResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateInstanceRealNameRequestContract>)) as ILogger<IUpdateInstanceRealNameRequestContract>,
                    new TransactedUseCasePipeNode<IUpdateInstanceRealNameRequestContract, IUpdateInstanceRealNameResultContract>(dbContext,
                        new UpdateInstanceRealNameUseCase(dbContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}