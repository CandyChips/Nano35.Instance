using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsEmail
{
    public class UpdateClientsEmailConsumer : IConsumer<IUpdateClientsEmailRequestContract>
    {
        private readonly IServiceProvider  _services;
        public UpdateClientsEmailConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IUpdateClientsEmailRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var result = 
                await new LoggedUseCasePipeNode<IUpdateClientsEmailRequestContract, IUpdateClientsEmailResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateClientsEmailRequestContract>)) as ILogger<IUpdateClientsEmailRequestContract>,
                    new TransactedUseCasePipeNode<IUpdateClientsEmailRequestContract, IUpdateClientsEmailResultContract>(dbContext,
                        new UpdateClientsEmailUseCase(dbContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}