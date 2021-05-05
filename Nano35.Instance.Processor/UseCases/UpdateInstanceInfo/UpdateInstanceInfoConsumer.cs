using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateInstanceInfo
{
    public class UpdateInstanceInfoConsumer : IConsumer<IUpdateInstanceInfoRequestContract>
    {
        private readonly IServiceProvider  _services;
        public UpdateInstanceInfoConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IUpdateInstanceInfoRequestContract> context)
        {
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var result = 
                await new LoggedUseCasePipeNode<IUpdateInstanceInfoRequestContract, IUpdateInstanceInfoResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateInstanceInfoRequestContract>)) as ILogger<IUpdateInstanceInfoRequestContract>,
                        new TransactedUseCasePipeNode<IUpdateInstanceInfoRequestContract, IUpdateInstanceInfoResultContract>(dbContext,
                            new UpdateInstanceInfoUseCase(dbContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}