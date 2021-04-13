using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateWorkersRole
{
    public class UpdateWorkersRoleConsumer : 
        IConsumer<IUpdateWorkersRoleRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdateWorkersRoleConsumer(IServiceProvider services)
        {
            _services = services;
        }

        public async Task Consume(ConsumeContext<IUpdateWorkersRoleRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var result = await new LoggedPipeNode<IUpdateWorkersRoleRequestContract, IUpdateWorkersRoleResultContract>(
                _services.GetService(typeof(ILogger<IUpdateWorkersRoleRequestContract>)) as ILogger<IUpdateWorkersRoleRequestContract>,
                new TransactedPipeNode<IUpdateWorkersRoleRequestContract, IUpdateWorkersRoleResultContract>(dbContext,
                    new UpdateWorkersRoleUseCase(dbContext)))
                .Ask(context.Message, context.CancellationToken);
            switch (result)
            {
                case IUpdateWorkersRoleSuccessResultContract:
                    await context.RespondAsync<IUpdateWorkersRoleSuccessResultContract>(result);
                    break;
                case IUpdateWorkersRoleErrorResultContract:
                    await context.RespondAsync<IUpdateWorkersRoleErrorResultContract>(result);
                    break;
            }
        }
    }
}