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
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<IGetAllWorkerRolesRequestContract>) _services.GetService(typeof(ILogger<IGetAllWorkerRolesRequestContract>));
            
            var message = context.Message;
            
            var result =
                await new LoggedPipeNode<IGetAllWorkerRolesRequestContract, IGetAllWorkerRolesResultContract>(logger,
                    new ValidatedGetAllWorkerRolesRequest(
                        new GetAllWorkerRolesUseCase(dbContext))).Ask(message, context.CancellationToken);
            
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