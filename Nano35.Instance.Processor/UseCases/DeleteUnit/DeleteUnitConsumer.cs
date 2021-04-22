using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.DeleteUnit
{
    public class DeleteUnitConsumer : 
        IConsumer<IDeleteUnitRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public DeleteUnitConsumer(IServiceProvider services) { _services = services; }
        
        public async Task Consume(ConsumeContext<IDeleteUnitRequestContract> context)
        {
            var result = await new LoggedPipeNode<IDeleteUnitRequestContract, IDeleteUnitResultContract>(
                _services.GetService(typeof(ILogger<IDeleteUnitRequestContract>)) as ILogger<IDeleteUnitRequestContract>,
                new DeleteUnitUseCase(
                    _services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                .Ask(context.Message, context.CancellationToken);
            
            switch (result)
            {
                case IDeleteUnitSuccessResultContract:
                    await context.RespondAsync<IDeleteUnitSuccessResultContract>(result);
                    break;
                case IDeleteUnitErrorResultContract:
                    await context.RespondAsync<IDeleteUnitErrorResultContract>(result);
                    break;
            }
        }
    }
}