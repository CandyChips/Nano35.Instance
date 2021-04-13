using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.UpdateUnitsAddress
{
    public class UpdateUnitsAddressConsumer : 
        IConsumer<IUpdateUnitsAddressRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public UpdateUnitsAddressConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(ConsumeContext<IUpdateUnitsAddressRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var result = await new LoggedPipeNode<IUpdateUnitsAddressRequestContract, IUpdateUnitsAddressResultContract>(
                _services.GetService(typeof(ILogger<IUpdateUnitsAddressRequestContract>)) as ILogger<IUpdateUnitsAddressRequestContract>,
                    new TransactedPipeNode<IUpdateUnitsAddressRequestContract, IUpdateUnitsAddressResultContract>(dbContext,
                        new UpdateUnitsAddressUseCase(dbContext)))
                .Ask(context.Message, context.CancellationToken);
            switch (result)
            {
                case IUpdateUnitsAddressSuccessResultContract:
                    await context.RespondAsync<IUpdateUnitsAddressSuccessResultContract>(result);
                    break;
                case IUpdateUnitsAddressErrorResultContract:
                    await context.RespondAsync<IUpdateUnitsAddressErrorResultContract>(result);
                    break;
            }
        }
    }
}