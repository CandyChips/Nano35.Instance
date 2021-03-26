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
            var dbcontect = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var logger = (ILogger<LoggedUpdateWorkersCommentRequest>) _services.GetService(typeof(ILogger<LoggedUpdateWorkersCommentRequest>));
            
            var message = context.Message;
            
            var result =
                await new LoggedUpdateWorkersCommentRequest(logger,
                    new ValidatedUpdateWorkersCommentRequest(
                        new TransactedUpdateWorkersCommentRequest(dbcontect,
                            new UpdateWorkersCommentUseCase(dbcontect)))).Ask(message, context.CancellationToken);
            
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