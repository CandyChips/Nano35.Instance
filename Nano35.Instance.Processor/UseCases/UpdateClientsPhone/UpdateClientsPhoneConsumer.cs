using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsPhone
{
    public class UpdateClientsPhoneConsumer : 
        IConsumer<IUpdateClientsPhoneRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdateClientsPhoneConsumer(
            IServiceProvider services)
        {
            _services = services;
        }


        public async Task Consume(
            ConsumeContext<IUpdateClientsPhoneRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var logger = (ILogger<IUpdateClientsPhoneRequestContract>) _services.GetService(typeof(ILogger<IUpdateClientsPhoneRequestContract>));
            
            var message = context.Message;
            
            var result =
                await new LoggedPipeNode<IUpdateClientsPhoneRequestContract, IUpdateClientsPhoneResultContract>(logger,
                        new TransactedPipeNode<IUpdateClientsPhoneRequestContract, IUpdateClientsPhoneResultContract>(dbContext,
                            new UpdateClientsPhoneUseCase(dbContext))).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IUpdateClientsPhoneSuccessResultContract:
                    await context.RespondAsync<IUpdateClientsPhoneSuccessResultContract>(result);
                    break;
                case IUpdateClientsPhoneErrorResultContract:
                    await context.RespondAsync<IUpdateClientsPhoneErrorResultContract>(result);
                    break;
            }
        }
    }
}