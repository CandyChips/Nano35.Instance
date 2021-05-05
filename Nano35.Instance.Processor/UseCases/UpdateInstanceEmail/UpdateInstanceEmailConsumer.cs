using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateInstanceEmail
{
    public class UpdateInstanceEmailConsumer : IConsumer<IUpdateInstanceEmailRequestContract>
    {
        private readonly IServiceProvider  _services;
        public UpdateInstanceEmailConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IUpdateInstanceEmailRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var result = 
                await new LoggedUseCasePipeNode<IUpdateInstanceEmailRequestContract, IUpdateInstanceEmailResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateInstanceEmailRequestContract>)) as ILogger<IUpdateInstanceEmailRequestContract>,
                        new TransactedUseCasePipeNode<IUpdateInstanceEmailRequestContract, IUpdateInstanceEmailResultContract>(dbContext,
                            new UpdateInstanceEmailUseCase(dbContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}