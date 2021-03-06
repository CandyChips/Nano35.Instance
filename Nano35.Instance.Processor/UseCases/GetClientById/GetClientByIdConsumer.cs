using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetClientById
{
    public class GetClientByIdConsumer : IConsumer<IGetClientByIdRequestContract>
    {
        private readonly IServiceProvider  _services;
        public GetClientByIdConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IGetClientByIdRequestContract> context)
        {
            var result = 
                await new LoggedPipeNode<IGetClientByIdRequestContract, IGetClientByIdResultContract>(
                    _services.GetService(typeof(ILogger<IGetClientByIdRequestContract>)) as ILogger<IGetClientByIdRequestContract>,
                        new GetClientById(_services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}