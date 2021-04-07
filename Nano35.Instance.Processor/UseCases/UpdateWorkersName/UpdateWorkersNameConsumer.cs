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
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var logger = (ILogger<IUpdateWorkersNameRequestContract>) _services.GetService(typeof(ILogger<IUpdateWorkersNameRequestContract>));
            
            var message = context.Message;
            
            var result =
                await new LoggedPipeNode<IUpdateWorkersNameRequestContract, IUpdateWorkersNameResultContract>(logger,
                    new TransactedPipeNode<IUpdateWorkersNameRequestContract, IUpdateWorkersNameResultContract>(dbContext,
                        new UpdateWorkersNameUseCase(dbContext))).Ask(message, context.CancellationToken);
            
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