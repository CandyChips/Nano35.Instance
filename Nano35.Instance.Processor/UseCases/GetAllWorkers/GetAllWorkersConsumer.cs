using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllWorkers
{
    public class GetAllWorkersConsumer : 
        IConsumer<IGetAllWorkersRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public GetAllWorkersConsumer(IServiceProvider services) { _services = services; }
        
        public async Task Consume(ConsumeContext<IGetAllWorkersRequestContract> context)
        {
            var result = await new LoggedPipeNode<IGetAllWorkersRequestContract, IGetAllWorkersResultContract>(
                _services.GetService(typeof(ILogger<IGetAllWorkersRequestContract>)) as ILogger<IGetAllWorkersRequestContract>,
                    new GetAllWorkersUseCase(
                        _services.GetService(typeof(ApplicationContext)) as ApplicationContext, 
                        _services.GetService(typeof(IBus)) as IBus))
                .Ask(context.Message, context.CancellationToken);
            
            switch (result)
            {
                case IGetAllWorkersSuccessResultContract:
                    await context.RespondAsync<IGetAllWorkersSuccessResultContract>(result);
                    break;
                case IGetAllWorkersErrorResultContract:
                    await context.RespondAsync<IGetAllWorkersErrorResultContract>(result);
                    break;
            }
        }
    }
}