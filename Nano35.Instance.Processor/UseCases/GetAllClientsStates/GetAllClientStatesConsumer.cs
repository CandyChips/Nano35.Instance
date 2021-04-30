using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllClientsStates
{
    public class GetAllClientStatesConsumer : 
        IConsumer<IGetAllClientStatesRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public GetAllClientStatesConsumer(IServiceProvider services) => _services = services;

        public async Task Consume(ConsumeContext<IGetAllClientStatesRequestContract> context)
        {
            var result = await new LoggedPipeNode<IGetAllClientStatesRequestContract, IGetAllClientStatesResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllClientStatesRequestContract>)) as ILogger<IGetAllClientStatesRequestContract>,
                    new GetAllClientStatesUseCase(
                        _services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                .Ask(context.Message, context.CancellationToken);
            
            switch (result)
            {
                case IGetAllClientStatesSuccessResultContract:
                    await context.RespondAsync<IGetAllClientStatesSuccessResultContract>(result);
                    break;
                case IGetAllClientStatesErrorResultContract:
                    await context.RespondAsync<IGetAllClientStatesErrorResultContract>(result);
                    break;
            }
        }
    }
}