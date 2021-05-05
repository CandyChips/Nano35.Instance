using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllClients
{
    public class GetAllClientsConsumer : IConsumer<IGetAllClientsRequestContract>
    {
        private readonly IServiceProvider  _services;
        public GetAllClientsConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IGetAllClientsRequestContract> context)
        {
            var result =
                await new LoggedUseCasePipeNode<IGetAllClientsRequestContract, IGetAllClientsResultContract>(
                    _services.GetService(typeof(ILogger<IGetAllClientsRequestContract>)) as ILogger<IGetAllClientsRequestContract>,
                        new GetAllClientsUseCase(_services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}