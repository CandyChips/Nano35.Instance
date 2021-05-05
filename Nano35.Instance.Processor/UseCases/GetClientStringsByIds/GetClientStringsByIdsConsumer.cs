using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetClientStringsByIds
{
    public class GetClientStringsByIdsConsumer : IConsumer<IGetClientStringsByIdsRequestContract>
    {
        private readonly IServiceProvider  _services;
        public GetClientStringsByIdsConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IGetClientStringsByIdsRequestContract> context)
        {
            var result = 
                await new LoggedUseCasePipeNode<IGetClientStringsByIdsRequestContract, IGetClientStringsByIdsSuccessResultContract>(
                _services.GetService(typeof(ILogger<IGetClientStringsByIdsRequestContract>)) as ILogger<IGetClientStringsByIdsRequestContract>,
                    new GetClientStringsByIdsUseCase(_services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}