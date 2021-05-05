using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetWorkerById
{
    public class GetWorkerByIdConsumer : IConsumer<IGetWorkerByIdRequestContract>
    {
        private readonly IServiceProvider  _services;
        public GetWorkerByIdConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IGetWorkerByIdRequestContract> context)
        { 
            var result = 
                await new LoggedUseCasePipeNode<IGetWorkerByIdRequestContract, IGetWorkerByIdSuccessResultContract>(
                    _services.GetService(typeof(ILogger<IGetWorkerByIdRequestContract>)) as ILogger<IGetWorkerByIdRequestContract>,
                        new GetWorkerByIdUseCase(_services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}