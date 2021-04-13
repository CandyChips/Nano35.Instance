using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateUnitsType
{
    public class UpdateUnitsTypeConsumer : 
        IConsumer<IUpdateUnitsTypeRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdateUnitsTypeConsumer(
            IServiceProvider services)
        {
            _services = services;
        }


        public async Task Consume(
            ConsumeContext<IUpdateUnitsTypeRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var logger = (ILogger<IUpdateUnitsTypeRequestContract>) _services.GetService(typeof(ILogger<IUpdateUnitsTypeRequestContract>));
            
            var message = context.Message;
            
            var result =
                await new LoggedPipeNode<IUpdateUnitsTypeRequestContract, IUpdateUnitsTypeResultContract>(logger,
                    new TransactedPipeNode<IUpdateUnitsTypeRequestContract, IUpdateUnitsTypeResultContract>(dbContext,
                        new UpdateUnitsTypeUseCase(dbContext))).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IUpdateUnitsTypeSuccessResultContract:
                    await context.RespondAsync<IUpdateUnitsTypeSuccessResultContract>(result);
                    break;
                case IUpdateUnitsTypeErrorResultContract:
                    await context.RespondAsync<IUpdateUnitsTypeErrorResultContract>(result);
                    break;
            }
        }
    }
}