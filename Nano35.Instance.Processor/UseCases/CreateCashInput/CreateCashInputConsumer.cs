using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.CreateCashInput
{
    public class CreateCashInputConsumer : 
        IConsumer<ICreateCashInputRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public CreateCashInputConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<ICreateCashInputRequestContract> context)
        {
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedCreateCashInputRequest>) _services.GetService(typeof(ILogger<LoggedCreateCashInputRequest>));

            var message = context.Message;
            
            var result =
                await new LoggedCreateCashInputRequest(logger,
                        new ValidatedCreateCashInputRequest(
                            new TransactedCreateCashInputRequest(dbContext,
                                new CreateCashInputUseCase(dbContext))))
                    .Ask(message, context.CancellationToken);
            switch (result)
            {
                case ICreateCashInputSuccessResultContract:
                    await context.RespondAsync<ICreateCashInputSuccessResultContract>(result);
                    break;
                case ICreateCashInputErrorResultContract:
                    await context.RespondAsync<ICreateCashInputErrorResultContract>(result);
                    break;
            }
        }
    }
}