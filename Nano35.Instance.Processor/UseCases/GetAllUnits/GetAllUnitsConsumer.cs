using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllUnits
{
    public class GetAllUnitsConsumer : 
        IConsumer<IGetAllUnitsRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public GetAllUnitsConsumer(IServiceProvider services) { _services = services; }
        
        public async Task Consume(ConsumeContext<IGetAllUnitsRequestContract> context)
        {
            var result = await new LoggedPipeNode<IGetAllUnitsRequestContract, IGetAllUnitsResultContract>(
                _services.GetService(typeof(ILogger<IGetAllUnitsRequestContract>)) as ILogger<IGetAllUnitsRequestContract>,
                new GetAllUnitsUseCase(
                    _services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                .Ask(context.Message, context.CancellationToken);
            
            switch (result)
            {
                case IGetAllUnitsSuccessResultContract:
                    await context.RespondAsync<IGetAllUnitsSuccessResultContract>(result);
                    break;
                case IGetAllUnitsErrorResultContract:
                    await context.RespondAsync<IGetAllUnitsErrorResultContract>(result);
                    break;
            }
        }
    }
}