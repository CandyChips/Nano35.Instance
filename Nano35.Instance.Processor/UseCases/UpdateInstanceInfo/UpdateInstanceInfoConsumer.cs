using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateInstanceInfo
{
    public class UpdateInstanceInfoConsumer : 
        IConsumer<IUpdateInstanceInfoRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdateInstanceInfoConsumer(
            IServiceProvider services)
        {
            _services = services;
        }


        public async Task Consume(
            ConsumeContext<IUpdateInstanceInfoRequestContract> context)
        {
            var dbcontect = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var logger = (ILogger<LoggedUpdateInstanceInfoRequest>) _services.GetService(typeof(ILogger<LoggedUpdateInstanceInfoRequest>));
            
            var message = context.Message;
            
            var result =
                await new LoggedUpdateInstanceInfoRequest(logger,
                    new ValidatedUpdateInstanceInfoRequest(
                        new TransactedUpdateInstanceInfoRequest(dbcontect,
                            new UpdateInstanceInfoRequest(dbcontect)))).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IUpdateInstanceInfoSuccessResultContract:
                    await context.RespondAsync<IUpdateInstanceInfoSuccessResultContract>(result);
                    break;
                case IUpdateInstanceInfoErrorResultContract:
                    await context.RespondAsync<IUpdateInstanceInfoErrorResultContract>(result);
                    break;
            }
        }
    }
}