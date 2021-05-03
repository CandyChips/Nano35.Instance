using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.DeleteUnit
{
    public class DeleteUnitConsumer : 
        IConsumer<IDeleteUnitRequestContract>
    {
        private readonly IServiceProvider  _services;
        public DeleteUnitConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IDeleteUnitRequestContract> context)
        {
            var result = 
                await new LoggedUseCasePipeNode<IDeleteUnitRequestContract, IDeleteUnitSuccessResultContract>(
                    _services.GetService(typeof(ILogger<IDeleteUnitRequestContract>)) as ILogger<IDeleteUnitRequestContract>,
                    new DeleteUnitUseCase(
                        _services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}