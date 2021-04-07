using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateUnitsPhone
{
    public class UpdateUnitsPhoneConsumer : 
        IConsumer<IUpdateUnitsPhoneRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdateUnitsPhoneConsumer(
            IServiceProvider services)
        {
            _services = services;
        }


        public async Task Consume(
            ConsumeContext<IUpdateUnitsPhoneRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var logger = (ILogger<IUpdateUnitsPhoneRequestContract>) _services.GetService(typeof(ILogger<IUpdateUnitsPhoneRequestContract>));
            
            var message = context.Message;
            
            var result =
                await new LoggedPipeNode<IUpdateUnitsPhoneRequestContract, IUpdateUnitsPhoneResultContract>(logger,
                    new ValidatedUpdateUnitsPhoneRequest(
                        new TransactedPipeNode<IUpdateUnitsPhoneRequestContract, IUpdateUnitsPhoneResultContract>(dbContext,
                            new UpdateUnitsPhoneUseCase(dbContext)))).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IUpdateUnitsPhoneSuccessResultContract:
                    await context.RespondAsync<IUpdateUnitsPhoneSuccessResultContract>(result);
                    break;
                case IUpdateUnitsPhoneErrorResultContract:
                    await context.RespondAsync<IUpdateUnitsPhoneErrorResultContract>(result);
                    break;
            }
        }
    }
}