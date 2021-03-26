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
        
        public UpdateWorkersRoleConsumer(
            IServiceProvider services)
        {
            _services = services;
        }


        public async Task Consume(
            ConsumeContext<IUpdateWorkersRoleRequestContract> context)
        {
            var dbcontect = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var logger = (ILogger<LoggedUpdateWorkersRoleRequest>) _services.GetService(typeof(ILogger<LoggedUpdateWorkersRoleRequest>));
            
            var message = context.Message;
            
            var result =
                await new LoggedUpdateWorkersRoleRequest(logger,
                    new ValidatedUpdateWorkersRoleRequest(
                        new TransactedUpdateWorkersRoleRequest(dbcontect,
                            new UpdateWorkersRoleUseCase(dbcontect)))).Ask(message, context.CancellationToken);
            
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