using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateUnitsName
{
    public class UpdateUnitsNameConsumer : 
        IConsumer<IUpdateUnitsNameRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdateUnitsNameConsumer(IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(ConsumeContext<IUpdateUnitsNameRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var result = await new LoggedPipeNode<IUpdateUnitsNameRequestContract, IUpdateUnitsNameResultContract>(
                _services.GetService(typeof(ILogger<IUpdateUnitsNameRequestContract>)) as ILogger<IUpdateUnitsNameRequestContract>,
                new TransactedPipeNode<IUpdateUnitsNameRequestContract, IUpdateUnitsNameResultContract>(dbContext, 
                    new UpdateUnitsNameUseCase(dbContext)))
                .Ask(context.Message, context.CancellationToken);
            switch (result)
            {
                case IUpdateUnitsNameSuccessResultContract:
                    await context.RespondAsync<IUpdateUnitsNameSuccessResultContract>(result);
                    break;
                case IUpdateUnitsNameErrorResultContract:
                    await context.RespondAsync<IUpdateUnitsNameErrorResultContract>(result);
                    break;
            }
        }
    }
}