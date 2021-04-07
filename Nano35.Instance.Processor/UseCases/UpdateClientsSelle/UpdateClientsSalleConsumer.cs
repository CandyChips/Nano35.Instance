using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsSelle
{
    public class UpdateClientsSelleConsumer : 
        IConsumer<IUpdateClientsSelleRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdateClientsSelleConsumer(
            IServiceProvider services)
        {
            _services = services;
        }


        public async Task Consume(
            ConsumeContext<IUpdateClientsSelleRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var logger = (ILogger<IUpdateClientsSelleRequestContract>) _services.GetService(typeof(ILogger<IUpdateClientsSelleRequestContract>));
            
            var message = context.Message;
            
            var result =
                await new LoggedPipeNode<IUpdateClientsSelleRequestContract, IUpdateClientsSelleResultContract>(logger,
                        new TransactedPipeNode<IUpdateClientsSelleRequestContract, IUpdateClientsSelleResultContract>(dbContext,
                            new UpdateClientsSelleUseCase(dbContext))).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IUpdateClientsSelleSuccessResultContract:
                    await context.RespondAsync<IUpdateClientsSelleSuccessResultContract>(result);
                    break;
                case IUpdateClientsSelleErrorResultContract:
                    await context.RespondAsync<IUpdateClientsSelleErrorResultContract>(result);
                    break;
            }
        }
    }
}