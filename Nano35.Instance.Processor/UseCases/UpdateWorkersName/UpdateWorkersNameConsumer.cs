using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateWorkersName
{
    public class UpdateWorkersNameConsumer : 
        IConsumer<IUpdateWorkersNameRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdateWorkersNameConsumer(
            IServiceProvider services)
        {
            _services = services;
        }


        public async Task Consume(
            ConsumeContext<IUpdateWorkersNameRequestContract> context)
        {
            var dbcontect = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var logger = (ILogger<LoggedUpdateWorkersNameRequest>) _services.GetService(typeof(ILogger<LoggedUpdateWorkersNameRequest>));
            
            var message = context.Message;
            
            var result =
                await new LoggedUpdateWorkersNameRequest(logger,
                    new ValidatedUpdateWorkersNameRequest(
                        new TransactedUpdateWorkersNameRequest(dbcontect,
                            new UpdateWorkersNameUseCase(dbcontect)))).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IUpdateWorkersNameSuccessResultContract:
                    await context.RespondAsync<IUpdateWorkersNameSuccessResultContract>(result);
                    break;
                case IUpdateWorkersNameErrorResultContract:
                    await context.RespondAsync<IUpdateWorkersNameErrorResultContract>(result);
                    break;
            }
        }
    }
}