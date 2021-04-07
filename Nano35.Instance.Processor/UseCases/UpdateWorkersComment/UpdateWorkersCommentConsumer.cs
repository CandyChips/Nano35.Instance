using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateWorkersComment
{
    public class UpdateWorkersCommentConsumer : 
        IConsumer<IUpdateWorkersCommentRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdateWorkersCommentConsumer(
            IServiceProvider services)
        {
            _services = services;
        }


        public async Task Consume(
            ConsumeContext<IUpdateWorkersCommentRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var logger = (ILogger<IUpdateWorkersCommentRequestContract>) _services.GetService(typeof(ILogger<IUpdateWorkersCommentRequestContract>));
            
            var message = context.Message;
            
            var result =
                await new LoggedPipeNode<IUpdateWorkersCommentRequestContract, IUpdateWorkersCommentResultContract>(logger,
                    new TransactedPipeNode<IUpdateWorkersCommentRequestContract, IUpdateWorkersCommentResultContract>(dbContext, 
                        new UpdateWorkersCommentUseCase(dbContext))).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IUpdateWorkersCommentSuccessResultContract:
                    await context.RespondAsync<IUpdateWorkersCommentSuccessResultContract>(result);
                    break;
                case IUpdateWorkersCommentErrorResultContract:
                    await context.RespondAsync<IUpdateWorkersCommentErrorResultContract>(result);
                    break;
            }
        }
    }
}