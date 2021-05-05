using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.CreateUnit
{
    public class CreateUnitConsumer : IConsumer<ICreateUnitRequestContract>
    {
        private readonly IServiceProvider  _services;
        public CreateUnitConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<ICreateUnitRequestContract> context)
        {
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var result = 
                await new LoggedUseCasePipeNode<ICreateUnitRequestContract, ICreateUnitSuccessResultContract>(
                    _services.GetService(typeof(ILogger<ICreateUnitRequestContract>)) as ILogger<ICreateUnitRequestContract>,
                        new TransactedUseCasePipeNode<ICreateUnitRequestContract, ICreateUnitSuccessResultContract>(
                            dbContext,
                            new CreateUnitUseCase(
                                dbContext,
                                _services.GetService(typeof(IBus)) as IBus)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}