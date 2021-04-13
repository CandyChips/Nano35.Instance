using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsName
{
    public class UpdateClientsNameConsumer : 
        IConsumer<IUpdateClientsNameRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdateClientsNameConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(ConsumeContext<IUpdateClientsNameRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var result = await new LoggedPipeNode<IUpdateClientsNameRequestContract, IUpdateClientsNameResultContract>(
                _services.GetService(typeof(ILogger<IUpdateClientsNameRequestContract>)) as ILogger<IUpdateClientsNameRequestContract>,
                new TransactedPipeNode<IUpdateClientsNameRequestContract, IUpdateClientsNameResultContract>(dbContext,
                    new UpdateClientsNameUseCase(dbContext)))
                .Ask(context.Message, context.CancellationToken);
            switch (result)
            {
                case IUpdateClientsNameSuccessResultContract:
                    await context.RespondAsync<IUpdateClientsNameSuccessResultContract>(result);
                    break;
                case IUpdateClientsNameErrorResultContract:
                    await context.RespondAsync<IUpdateClientsNameErrorResultContract>(result);
                    break;
            }
        }
    }
}