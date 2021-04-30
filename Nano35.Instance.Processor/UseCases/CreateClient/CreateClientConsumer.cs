using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.CreateClient
{
    public class CreateClientConsumer : 
        IConsumer<ICreateClientRequestContract>
    {
        private readonly IServiceProvider _services;
        
        public CreateClientConsumer(IServiceProvider services) => _services = services;

        public async Task Consume(ConsumeContext<ICreateClientRequestContract> context)
        {
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var result = await new LoggedPipeNode<ICreateClientRequestContract, ICreateClientResultContract>(
                _services.GetService(typeof(ILogger<ICreateClientRequestContract>)) as ILogger<ICreateClientRequestContract>,
                        new TransactedPipeNode<ICreateClientRequestContract, ICreateClientResultContract>(
                            dbContext,
                            new CreateClientUseCase(dbContext)))
                .Ask(context.Message, context.CancellationToken);
            switch (result)
            {
                case ICreateClientSuccessResultContract:
                    await context.RespondAsync<ICreateClientSuccessResultContract>(result);
                    break;
                case ICreateClientErrorResultContract:
                    await context.RespondAsync<ICreateClientErrorResultContract>(result);
                    break;
            }
        }
    }
}