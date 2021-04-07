using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsState
{
    public class UpdateClientsStateConsumer : 
        IConsumer<IUpdateClientsStateRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdateClientsStateConsumer(
            IServiceProvider services)
        {
            _services = services;
        }


        public async Task Consume(
            ConsumeContext<IUpdateClientsStateRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var logger = (ILogger<IUpdateClientsStateRequestContract>) _services.GetService(typeof(ILogger<IUpdateClientsStateRequestContract>));
            
            var message = context.Message;
            
            var result =
                await new LoggedPipeNode<IUpdateClientsStateRequestContract, IUpdateClientsStateResultContract>(logger,
                    new ValidatedUpdateClientsStateRequest(
                        new TransactedPipeNode<IUpdateClientsStateRequestContract, IUpdateClientsStateResultContract>(dbContext,
                            new UpdateClientsStateUseCase(dbContext)))).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IUpdateClientsStateSuccessResultContract:
                    await context.RespondAsync<IUpdateClientsStateSuccessResultContract>(result);
                    break;
                case IUpdateClientsStateErrorResultContract:
                    await context.RespondAsync<IUpdateClientsStateErrorResultContract>(result);
                    break;
            }
        }
    }
}