using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllWorkerRoles
{
    public class GetAllWorkerRolesConsumer : 
        IConsumer<IGetAllWorkerRolesRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public GetAllWorkerRolesConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllWorkerRolesRequestContract> context)
        {
            var dbcontect = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedGetAllWorkerRolesRequest>) _services.GetService(typeof(ILogger<LoggedGetAllWorkerRolesRequest>));
            
            var message = context.Message;
            
            var result =
                await new LoggedGetAllWorkerRolesRequest(logger,
                    new ValidatedGetAllWorkerRolesRequest(
                        new GetAllWorkerRolesRequest(dbcontect))
                ).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IGetAllWorkerRolesSuccessResultContract:
                    await context.RespondAsync<IGetAllWorkerRolesSuccessResultContract>(result);
                    break;
                case IGetAllWorkerRolesErrorResultContract:
                    await context.RespondAsync<IGetAllWorkerRolesErrorResultContract>(result);
                    break;
            }
        }
    }
}