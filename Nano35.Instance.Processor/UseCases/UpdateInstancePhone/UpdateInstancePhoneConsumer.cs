using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateInstancePhone
{
    public class UpdateInstancePhoneConsumer : 
        IConsumer<IUpdateInstancePhoneRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdateInstancePhoneConsumer(
            IServiceProvider services)
        {
            _services = services;
        }


        public async Task Consume(
            ConsumeContext<IUpdateInstancePhoneRequestContract> context)
        {
            var dbcontect = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var logger = (ILogger<LoggedUpdateInstancePhoneRequest>) _services.GetService(typeof(ILogger<LoggedUpdateInstancePhoneRequest>));
            
            var message = context.Message;
            
            var result =
                await new LoggedUpdateInstancePhoneRequest(logger,
                    new ValidatedUpdateInstancePhoneRequest(
                        new TransactedUpdateInstancePhoneRequest(dbcontect,
                            new UpdateInstancePhoneRequest(dbcontect)))
                ).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IUpdateInstancePhoneSuccessResultContract:
                    await context.RespondAsync<IUpdateInstancePhoneSuccessResultContract>(result);
                    break;
                case IUpdateInstancePhoneErrorResultContract:
                    await context.RespondAsync<IUpdateInstancePhoneErrorResultContract>(result);
                    break;
            }
        }
    }
}