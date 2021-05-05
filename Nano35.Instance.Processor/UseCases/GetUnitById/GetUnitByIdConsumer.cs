using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetUnitById
{
    public class GetUnitByIdConsumer : IConsumer<IGetUnitByIdRequestContract>
    {
        private readonly IServiceProvider  _services;
        public GetUnitByIdConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IGetUnitByIdRequestContract> context)
        {
            var result = 
                await new LoggedUseCasePipeNode<IGetUnitByIdRequestContract, IGetUnitByIdResultContract>(
                    _services.GetService(typeof(ILogger<IGetUnitByIdRequestContract>)) as ILogger<IGetUnitByIdRequestContract>,
                    new GetUnitByIdUseCase(_services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}