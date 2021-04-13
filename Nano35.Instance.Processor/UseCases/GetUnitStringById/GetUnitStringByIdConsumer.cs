using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetUnitStringById
{
    public class GetUnitStringByIdConsumer : 
        IConsumer<IGetUnitStringByIdRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public GetUnitStringByIdConsumer(IServiceProvider services)
        {
            _services = services;
        }

        public async Task Consume(ConsumeContext<IGetUnitStringByIdRequestContract> context)
        {
            var result = await new LoggedPipeNode<IGetUnitStringByIdRequestContract, IGetUnitStringByIdResultContract>(
                _services.GetService(typeof(ILogger<IGetUnitStringByIdRequestContract>)) as ILogger<IGetUnitStringByIdRequestContract>,
                    new GetUnitStringByIdUseCase(
                        _services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                .Ask(context.Message, context.CancellationToken);
            switch (result)
            {
                case IGetUnitStringByIdSuccessResultContract:
                    await context.RespondAsync<IGetUnitStringByIdSuccessResultContract>(result);
                    break;
                case IGetUnitStringByIdErrorResultContract:
                    await context.RespondAsync<IGetUnitStringByIdErrorResultContract>(result);
                    break;
            }
        }
    }
}