using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllRolesByUser
{
    public class GetAllRolesByUserConsumer : IConsumer<IGetAllRolesByUserRequestContract>
    {
        private readonly IServiceProvider  _services;
        public GetAllRolesByUserConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IGetAllRolesByUserRequestContract> context)
        {
            var result = await new LoggedPipeNode<IGetAllRolesByUserRequestContract, IGetAllRolesByUserResultContract>(
                _services.GetService(typeof(ILogger<IGetAllRolesByUserRequestContract>)) as ILogger<IGetAllRolesByUserRequestContract>,
                new GetAllRolesByUser(_services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}