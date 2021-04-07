using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsEmail
{
    public class UpdateClientsEmailConsumer : 
        IConsumer<IUpdateClientsEmailRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdateClientsEmailConsumer(
            IServiceProvider services)
        {
            _services = services;
        }


        public async Task Consume(
            ConsumeContext<IUpdateClientsEmailRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var logger = (ILogger<IUpdateClientsEmailRequestContract>) _services.GetService(typeof(ILogger<IUpdateClientsEmailRequestContract>));
            
            var message = context.Message;
            
            var result =
                await new LoggedPipeNode<IUpdateClientsEmailRequestContract, IUpdateClientsEmailResultContract>(logger,
                        new TransactedPipeNode<IUpdateClientsEmailRequestContract, IUpdateClientsEmailResultContract>(dbContext,
                            new UpdateClientsEmailUseCase(dbContext))).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IUpdateClientsEmailSuccessResultContract:
                    await context.RespondAsync<IUpdateClientsEmailSuccessResultContract>(result);
                    break;
                case IUpdateClientsEmailErrorResultContract:
                    await context.RespondAsync<IUpdateClientsEmailErrorResultContract>(result);
                    break;
            }
        }
    }
}