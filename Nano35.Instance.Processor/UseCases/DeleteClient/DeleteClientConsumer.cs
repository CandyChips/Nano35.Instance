using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.DeleteClient
{
    public class DeleteClientConsumer : 
        IConsumer<IDeleteClientRequestContract>
    {
        private readonly IServiceProvider  _services;
        public DeleteClientConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IDeleteClientRequestContract> context)
        {
            var result =
                await new LoggedUseCasePipeNode<IDeleteClientRequestContract, IDeleteClientSuccessResultContract>(
                        _services.GetService(typeof(ILogger<IDeleteClientRequestContract>)) as ILogger<IDeleteClientRequestContract>,
                        new DeleteClientUseCase(
                            _services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}