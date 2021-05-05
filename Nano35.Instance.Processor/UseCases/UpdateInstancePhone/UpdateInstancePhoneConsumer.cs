using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateInstancePhone
{
    public class UpdateInstancePhoneConsumer : IConsumer<IUpdateInstancePhoneRequestContract>
    {
        private readonly IServiceProvider  _services;
        public UpdateInstancePhoneConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IUpdateInstancePhoneRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var result =
                await new LoggedUseCasePipeNode<IUpdateInstancePhoneRequestContract, IUpdateInstancePhoneResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateInstancePhoneRequestContract>)) as ILogger<IUpdateInstancePhoneRequestContract>,
                    new TransactedUseCasePipeNode<IUpdateInstancePhoneRequestContract, IUpdateInstancePhoneResultContract>(dbContext,
                        new UpdateInstancePhoneUseCase(dbContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}