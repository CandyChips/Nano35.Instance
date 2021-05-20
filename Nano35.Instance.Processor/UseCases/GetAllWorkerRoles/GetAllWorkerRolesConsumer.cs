using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllWorkerRoles
{
    public class GetAllWorkerRolesConsumer : IConsumer<IGetAllWorkerRolesRequestContract>
    {
        private readonly IServiceProvider  _services;
        public GetAllWorkerRolesConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IGetAllWorkerRolesRequestContract> context)
        {
            var result = 
                await new LoggedPipeNode<IGetAllWorkerRolesRequestContract, IGetAllWorkerRolesResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllWorkerRolesRequestContract>)) as ILogger<IGetAllWorkerRolesRequestContract>,
                    new GetAllWorkerRoles(_services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}