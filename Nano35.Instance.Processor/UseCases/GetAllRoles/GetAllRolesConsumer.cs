using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllRoles
{
    public class GetAllRolesConsumer : IConsumer<IGetAllRolesRequestContract>
    {
        private readonly IServiceProvider  _services;
        public GetAllRolesConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IGetAllRolesRequestContract> context)
        {
            var result = await new LoggedUseCasePipeNode<IGetAllRolesRequestContract, IGetAllRolesSuccessResultContract>(
                _services.GetService(typeof(ILogger<IGetAllRolesRequestContract>)) as ILogger<IGetAllRolesRequestContract>,
                new GetAllRolesUseCase(_services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}