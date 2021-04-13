using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetClientStringById
{
    public class GetClientStringByIdConsumer : 
        IConsumer<IGetClientStringByIdRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public GetClientStringByIdConsumer(IServiceProvider services) { _services = services; }

        public async Task Consume(ConsumeContext<IGetClientStringByIdRequestContract> context)
        {
            var result = await new LoggedPipeNode<IGetClientStringByIdRequestContract, IGetClientStringByIdResultContract>(
                _services.GetService(typeof(ILogger<IGetClientStringByIdRequestContract>)) as ILogger<IGetClientStringByIdRequestContract>,
                new GetClientStringByIdUseCase(
                    _services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                .Ask(context.Message, context.CancellationToken);
            switch (result)
            {
                case IGetClientStringByIdSuccessResultContract:
                    await context.RespondAsync<IGetClientStringByIdSuccessResultContract>(result);
                    break;
                case IGetClientStringByIdErrorResultContract:
                    await context.RespondAsync<IGetClientStringByIdErrorResultContract>(result);
                    break;
            }
        }
    }
}