using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetInstanceStringsByIds
{
    public class GetInstanceStringsByIdsConsumer : 
        IConsumer<IGetInstanceStringsByIdsRequestContract>
    {
        private readonly IServiceProvider  _services;
        public GetInstanceStringsByIdsConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IGetInstanceStringsByIdsRequestContract> context)
        {
            var result =
                await new LoggedUseCasePipeNode<IGetInstanceStringsByIdsRequestContract, IGetInstanceStringsByIdsSuccessResultContract>(
                    _services.GetService(typeof(ILogger<IGetInstanceStringsByIdsRequestContract>)) as ILogger<IGetInstanceStringsByIdsRequestContract>,
                    new GetInstanceStringsByIdsUseCase(
                        _services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}