using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetInstanceStringById
{
    public class GetInstanceStringByIdConsumer : IConsumer<IGetInstanceStringByIdRequestContract>
    {
        private readonly IServiceProvider  _services;
        public GetInstanceStringByIdConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IGetInstanceStringByIdRequestContract> context)
        {
            var result =
                await new LoggedPipeNode<IGetInstanceStringByIdRequestContract, IGetInstanceStringByIdResultContract>(
                    _services.GetService(typeof(ILogger<IGetInstanceStringByIdRequestContract>)) as ILogger<IGetInstanceStringByIdRequestContract>,
                        new GetInstanceStringById(_services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}