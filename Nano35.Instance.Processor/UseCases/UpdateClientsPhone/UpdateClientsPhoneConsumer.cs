using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsPhone
{
    public class UpdateClientsPhoneConsumer : IConsumer<IUpdateClientsPhoneRequestContract>
    {
        private readonly IServiceProvider  _services;
        public UpdateClientsPhoneConsumer(IServiceProvider services) => _services = services;
        public async Task Consume(ConsumeContext<IUpdateClientsPhoneRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var result = 
                await new LoggedPipeNode<IUpdateClientsPhoneRequestContract, IUpdateClientsPhoneResultContract>(
                    _services.GetService(typeof(ILogger<IUpdateClientsPhoneRequestContract>)) as ILogger<IUpdateClientsPhoneRequestContract>,
                    new TransactedPipeNode<IUpdateClientsPhoneRequestContract, IUpdateClientsPhoneResultContract>(dbContext,
                        new UpdateClientsPhone(dbContext)))
                    .Ask(context.Message, context.CancellationToken);
            await context.RespondAsync(result);
        }
    }
}