using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.DeleteClient
{
    public class DeleteClientConsumer : 
        IConsumer<IDeleteClientRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public DeleteClientConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IDeleteClientRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<IDeleteClientRequestContract>) _services.GetService(typeof(ILogger<IDeleteClientRequestContract>));
            
            var message = context.Message;
            
            var result =
                await new LoggedPipeNode<IDeleteClientRequestContract, IDeleteClientResultContract>(logger,
                        new DeleteClientUseCase(dbContext)).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IDeleteClientSuccessResultContract:
                    await context.RespondAsync<IDeleteClientSuccessResultContract>(result);
                    break;
                case IDeleteClientErrorResultContract:
                    await context.RespondAsync<IDeleteClientErrorResultContract>(result);
                    break;
            }
        }
    }
}