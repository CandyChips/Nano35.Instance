using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllInstances
{
    public class GetAllInstancesConsumer : IConsumer<IGetAllInstancesRequestContract>
    {
        private readonly IServiceProvider  _services;
        public GetAllInstancesConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IGetAllInstancesRequestContract> context)
        {
            var result = 
                await new LoggedUseCasePipeNode<IGetAllInstancesRequestContract, IGetAllInstancesResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllInstancesRequestContract>)) as ILogger<IGetAllInstancesRequestContract>,
                    new GetAllInstancesUseCase(_services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}