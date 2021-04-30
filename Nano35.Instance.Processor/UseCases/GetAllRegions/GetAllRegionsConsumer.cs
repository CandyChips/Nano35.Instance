using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllRegions
{
    public class GetAllRegionsConsumer : 
        IConsumer<IGetAllRegionsRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public GetAllRegionsConsumer(IServiceProvider services) => _services = services;

        public async Task Consume(ConsumeContext<IGetAllRegionsRequestContract> context)
        {
            var result = await new LoggedPipeNode<IGetAllRegionsRequestContract, IGetAllRegionsResultContract>(
                _services.GetService(typeof(ILogger<IGetAllRegionsRequestContract>)) as ILogger<IGetAllRegionsRequestContract>,
                new GetAllRegionsUseCase(
                    _services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                .Ask(context.Message, context.CancellationToken);
            
            switch (result)
            {
                case IGetAllRegionsSuccessResultContract:
                    await context.RespondAsync<IGetAllRegionsSuccessResultContract>(result);
                    break;
                case IGetAllRegionsErrorResultContract:
                    await context.RespondAsync<IGetAllRegionsErrorResultContract>(result);
                    break;
            }
        }
    }
}