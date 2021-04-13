using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetInstanceById
{
    public class GetInstanceByIdConsumer : 
        IConsumer<IGetInstanceByIdRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public GetInstanceByIdConsumer(IServiceProvider services)
        {
            _services = services;
        }


        public async Task Consume(ConsumeContext<IGetInstanceByIdRequestContract> context)
        {
            var result = await new LoggedPipeNode<IGetInstanceByIdRequestContract, IGetInstanceByIdResultContract>(
                _services.GetService(typeof(ILogger<IGetInstanceByIdRequestContract>)) as ILogger<IGetInstanceByIdRequestContract>,
                    new GetInstanceByIdUseCase(
                        _services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                .Ask(context.Message, context.CancellationToken);
            switch (result)
            {
                case IGetInstanceByIdSuccessResultContract:
                    await context.RespondAsync<IGetInstanceByIdSuccessResultContract>(result);
                    break;
                case IGetInstanceByIdErrorResultContract:
                    await context.RespondAsync<IGetInstanceByIdErrorResultContract>(result);
                    break;
            }
        }
    }
}