using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateWorkersName
{
    public class UpdateWorkersNameConsumer : 
        IConsumer<IUpdateWorkersNameRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdateWorkersNameConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(ConsumeContext<IUpdateWorkersNameRequestContract> context)
        {
            var dbContext = _services.GetService(typeof(ApplicationContext)) as ApplicationContext;
            var result = await new LoggedPipeNode<IUpdateWorkersNameRequestContract, IUpdateWorkersNameResultContract>(
                _services.GetService(typeof(ILogger<IUpdateWorkersNameRequestContract>)) as ILogger<IUpdateWorkersNameRequestContract>,
                    new TransactedPipeNode<IUpdateWorkersNameRequestContract, IUpdateWorkersNameResultContract>(dbContext,
                        new UpdateWorkersNameUseCase(dbContext)))
                .Ask(context.Message, context.CancellationToken);
            switch (result)
            {
                case IUpdateWorkersNameSuccessResultContract:
                    await context.RespondAsync<IUpdateWorkersNameSuccessResultContract>(result);
                    break;
                case IUpdateWorkersNameErrorResultContract:
                    await context.RespondAsync<IUpdateWorkersNameErrorResultContract>(result);
                    break;
            }
        }
    }
}