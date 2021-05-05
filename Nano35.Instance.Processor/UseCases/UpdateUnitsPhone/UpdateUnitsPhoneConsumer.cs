using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateUnitsPhone
{
    public class UpdateUnitsPhoneConsumer : IConsumer<IUpdateUnitsPhoneRequestContract>
    {
        private readonly IServiceProvider  _services;
        public UpdateUnitsPhoneConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IUpdateUnitsPhoneRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var result =
                await new LoggedUseCasePipeNode<IUpdateUnitsPhoneRequestContract, IUpdateUnitsPhoneResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateUnitsPhoneRequestContract>)) as ILogger<IUpdateUnitsPhoneRequestContract>,
                    new TransactedUseCasePipeNode<IUpdateUnitsPhoneRequestContract, IUpdateUnitsPhoneResultContract>(dbContext,
                        new UpdateUnitsPhoneUseCase(dbContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}