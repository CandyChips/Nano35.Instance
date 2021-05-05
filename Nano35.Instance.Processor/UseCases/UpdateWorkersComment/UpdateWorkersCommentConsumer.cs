using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateWorkersComment
{
    public class UpdateWorkersCommentConsumer : IConsumer<IUpdateWorkersCommentRequestContract>
    {
        private readonly IServiceProvider  _services;
        public UpdateWorkersCommentConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IUpdateWorkersCommentRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var result = 
                await new LoggedUseCasePipeNode<IUpdateWorkersCommentRequestContract, IUpdateWorkersCommentResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateWorkersCommentRequestContract>)) as ILogger<IUpdateWorkersCommentRequestContract>,
                    new TransactedUseCasePipeNode<IUpdateWorkersCommentRequestContract, IUpdateWorkersCommentResultContract>(dbContext, 
                        new UpdateWorkersCommentUseCase(dbContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}