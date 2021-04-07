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
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var logger = (ILogger<IUpdateInstanceInfoRequestContract>) _services.GetService(typeof(ILogger<IUpdateInstanceInfoRequestContract>));
            
            var message = context.Message;
            
            var result =
                await new LoggedPipeNode<IUpdateInstanceInfoRequestContract, IUpdateInstanceInfoResultContract>(logger,
                    new ValidatedUpdateInstanceInfoRequest(
                        new TransactedPipeNode<IUpdateInstanceInfoRequestContract, IUpdateInstanceInfoResultContract>(dbContext,
                            new UpdateInstanceInfoUseCase(dbContext)))).Ask(message, context.CancellationToken);
            
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