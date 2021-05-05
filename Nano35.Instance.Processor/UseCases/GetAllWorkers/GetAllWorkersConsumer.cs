using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllWorkers
{
    public class GetAllWorkersConsumer : IConsumer<IGetAllWorkersRequestContract>
    {
        private readonly IServiceProvider  _services;
        public GetAllWorkersConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IGetAllWorkersRequestContract> context)
        {
            var result = 
                await new LoggedUseCasePipeNode<IGetAllWorkersRequestContract, IGetAllWorkersSuccessResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllWorkersRequestContract>)) as ILogger<IGetAllWorkersRequestContract>,
                        new GetAllWorkersUseCase(
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext, 
                            _services.GetService(typeof(IBus)) as IBus))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}