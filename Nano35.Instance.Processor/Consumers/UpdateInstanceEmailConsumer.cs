using System;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests;
using Nano35.Instance.Processor.Requests.UpdateInstanceEmail;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Consumers
{
    public class UpdateInstanceEmailConsumer : 
        IConsumer<IUpdateInstanceEmailRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdateInstanceEmailConsumer(
            IServiceProvider services)
        {
            _services = services;
        }


        public async Task Consume(
            ConsumeContext<IUpdateInstanceEmailRequestContract> context)
        {
            var dbcontect = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var logger = (ILogger<LoggedUpdateInstanceEmailRequest>) _services.GetService(typeof(ILogger<LoggedUpdateInstanceEmailRequest>));
            
            var message = context.Message;
            
            var result =
                await new LoggedUpdateInstanceEmailRequest(logger,
                    new ValidatedUpdateInstanceEmailRequest(
                        new TransactedUpdateInstanceEmailRequest(dbcontect,
                            new UpdateInstanceEmailRequest(dbcontect)))
                ).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IUpdateInstanceEmailSuccessResultContract:
                    await context.RespondAsync<IUpdateInstanceEmailSuccessResultContract>(result);
                    break;
                case IUpdateInstanceEmailErrorResultContract:
                    await context.RespondAsync<IUpdateInstanceEmailErrorResultContract>(result);
                    break;
            }
        }
    }
}