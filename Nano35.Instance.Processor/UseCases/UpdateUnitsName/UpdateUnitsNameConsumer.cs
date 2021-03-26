using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateUnitsName
{
    public class UpdateUnitsNameConsumer : 
        IConsumer<IUpdateUnitsNameRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdateUnitsNameConsumer(
            IServiceProvider services)
        {
            _services = services;
        }


        public async Task Consume(
            ConsumeContext<IUpdateUnitsNameRequestContract> context)
        {
            var dbcontect = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var logger = (ILogger<LoggedUpdateUnitsNameRequest>) _services.GetService(typeof(ILogger<LoggedUpdateUnitsNameRequest>));
            
            var message = context.Message;
            
            var result =
                await new LoggedUpdateUnitsNameRequest(logger,
                    new ValidatedUpdateUnitsNameRequest(
                        new TransactedUpdateUnitsNameRequest(dbcontect,
                            new UpdateUnitsNameUseCase(dbcontect)))).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IUpdateUnitsNameSuccessResultContract:
                    await context.RespondAsync<IUpdateUnitsNameSuccessResultContract>(result);
                    break;
                case IUpdateUnitsNameErrorResultContract:
                    await context.RespondAsync<IUpdateUnitsNameErrorResultContract>(result);
                    break;
            }
        }
    }
}