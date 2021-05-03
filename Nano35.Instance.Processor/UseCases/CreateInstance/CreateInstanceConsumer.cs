using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.CreateInstance
{
    public class CreateInstanceConsumer : 
        IConsumer<ICreateInstanceRequestContract>
    {
        private readonly IServiceProvider  _services;
        public CreateInstanceConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<ICreateInstanceRequestContract> context)
        {
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var result = 
                await new LoggedUseCasePipeNode<ICreateInstanceRequestContract, ICreateInstanceSuccessResultContract>(
                        _services.GetService(typeof(ILogger<ICreateInstanceRequestContract>)) as ILogger<ICreateInstanceRequestContract>,
                        new TransactedUseCasePipeNode<ICreateInstanceRequestContract, ICreateInstanceSuccessResultContract>(
                            dbContext,
                            new CreateInstanceUseCase(dbContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}