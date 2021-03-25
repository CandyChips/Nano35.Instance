using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateInstanceName
{
    public class UpdateInstanceNameConsumer : 
        IConsumer<IUpdateInstanceNameRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdateInstanceNameConsumer(
            IServiceProvider services)
        {
            _services = services;
        }


        public async Task Consume(
            ConsumeContext<IUpdateInstanceNameRequestContract> context)
        {
            var dbcontect = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var logger = (ILogger<LoggedUpdateInstanceNameRequest>) _services.GetService(typeof(ILogger<LoggedUpdateInstanceNameRequest>));
            
            var message = context.Message;
            
            var result =
                await new LoggedUpdateInstanceNameRequest(logger,
                    new ValidatedUpdateInstanceNameRequest(
                        new TransactedUpdateInstanceNameRequest(dbcontect,
                            new UpdateInstanceNameRequest(dbcontect)))).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IUpdateInstanceNameSuccessResultContract:
                    await context.RespondAsync<IUpdateInstanceNameSuccessResultContract>(result);
                    break;
                case IUpdateInstanceNameErrorResultContract:
                    await context.RespondAsync<IUpdateInstanceNameErrorResultContract>(result);
                    break;
            }
        }
    }
}