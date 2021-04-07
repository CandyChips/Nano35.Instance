using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateInstanceEmail
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
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var logger = (ILogger<IUpdateInstanceEmailRequestContract>) _services.GetService(typeof(ILogger<IUpdateInstanceEmailRequestContract>));
            
            var message = context.Message;
            
            var result =
                await new LoggedPipeNode<IUpdateInstanceEmailRequestContract, IUpdateInstanceEmailResultContract>(logger,
                    new ValidatedUpdateInstanceEmailRequest(
                        new TransactedPipeNode<IUpdateInstanceEmailRequestContract, IUpdateInstanceEmailResultContract>(dbContext,
                            new UpdateInstanceEmailUseCase(dbContext)))).Ask(message, context.CancellationToken);
            
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