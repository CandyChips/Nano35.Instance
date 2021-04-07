using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsType
{
    public class UpdateClientsTypeConsumer : 
        IConsumer<IUpdateClientsTypeRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdateClientsTypeConsumer(
            IServiceProvider services)
        {
            _services = services;
        }


        public async Task Consume(
            ConsumeContext<IUpdateClientsTypeRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var logger = (ILogger<IUpdateClientsTypeRequestContract>) _services.GetService(typeof(ILogger<IUpdateClientsTypeRequestContract>));
            
            var message = context.Message;
            
            var result =
                await new LoggedPipeNode<IUpdateClientsTypeRequestContract, IUpdateClientsTypeResultContract>(logger,
                    new ValidatedUpdateClientsTypeRequest(
                        new TransactedPipeNode<IUpdateClientsTypeRequestContract, IUpdateClientsTypeResultContract>(dbContext,
                            new UpdateClientsTypeUseCase(dbContext)))).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IUpdateClientsTypeSuccessResultContract:
                    await context.RespondAsync<IUpdateClientsTypeSuccessResultContract>(result);
                    break;
                case IUpdateClientsTypeErrorResultContract:
                    await context.RespondAsync<IUpdateClientsTypeErrorResultContract>(result);
                    break;
            }
        }
    }
}