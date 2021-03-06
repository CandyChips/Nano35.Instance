using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetClientStringById
{
    public class GetClientStringByIdConsumer : IConsumer<IGetClientStringByIdRequestContract>
    {
        private readonly IServiceProvider  _services;
        public GetClientStringByIdConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IGetClientStringByIdRequestContract> context)
        {
            var result = 
                await new LoggedPipeNode<IGetClientStringByIdRequestContract, IGetClientStringByIdResultContract>(
                    _services.GetService(typeof(ILogger<IGetClientStringByIdRequestContract>)) as ILogger<IGetClientStringByIdRequestContract>,
                    new GetClientStringById(_services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}