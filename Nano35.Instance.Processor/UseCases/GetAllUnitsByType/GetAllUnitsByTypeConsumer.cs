using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllUnitsByType
{
    public class GetAllUnitsByTypeConsumer : IConsumer<IGetAllUnitsByTypeRequestContract>
    {
        private readonly IServiceProvider  _services;
        public GetAllUnitsByTypeConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IGetAllUnitsByTypeRequestContract> context)
        {
            var result = 
                await new LoggedUseCasePipeNode<IGetAllUnitsByTypeRequestContract, IGetAllUnitsByTypeResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllUnitsByTypeRequestContract>)) as ILogger<IGetAllUnitsByTypeRequestContract>,
                    new GetAllUnitsByTypeUseCase(_services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}