using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateUnitsWorkingFormat
{
    public class UpdateUnitsWorkingFormatConsumer : 
        IConsumer<IUpdateUnitsWorkingFormatRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdateUnitsWorkingFormatConsumer(
            IServiceProvider services)
        {
            _services = services;
        }


        public async Task Consume(
            ConsumeContext<IUpdateUnitsWorkingFormatRequestContract> context)
        {
            var dbcontect = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var logger = (ILogger<LoggedUpdateUnitsWorkingFormatRequest>) _services.GetService(typeof(ILogger<LoggedUpdateUnitsWorkingFormatRequest>));
            
            var message = context.Message;
            
            var result =
                await new LoggedUpdateUnitsWorkingFormatRequest(logger,
                    new ValidatedUpdateUnitsWorkingFormatRequest(
                        new TransactedUpdateUnitsWorkingFormatRequest(dbcontect,
                            new UpdateUnitsWorkingFormatUseCase(dbcontect)))).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IUpdateUnitsWorkingFormatSuccessResultContract:
                    await context.RespondAsync<IUpdateUnitsWorkingFormatSuccessResultContract>(result);
                    break;
                case IUpdateUnitsWorkingFormatErrorResultContract:
                    await context.RespondAsync<IUpdateUnitsWorkingFormatErrorResultContract>(result);
                    break;
            }
        }
    }
}