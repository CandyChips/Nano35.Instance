using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.CreatePaymentOfSelle
{
    public class CreatePaymentOfSelleConsumer :
        IConsumer<ICreatePaymentOfSelleRequestContract>
    {
        private readonly IServiceProvider _services;

        public CreatePaymentOfSelleConsumer(
            IServiceProvider services)
        {
            _services = services;
        }

        public async Task Consume(
            ConsumeContext<ICreatePaymentOfSelleRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger =
                (ILogger<LoggedCreatePaymentOfSelleRequest>) _services.GetService(
                    typeof(ILogger<LoggedCreatePaymentOfSelleRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedCreatePaymentOfSelleRequest(logger,
                    new ValidatedCreatePaymentOfSelleRequest(
                        new TransactedCreatePaymentOfSelleRequest(dbContext,
                            new CreatePaymentOfSelleRequest(dbContext)))).Ask(message, context.CancellationToken);

            // Check response of create client request
            switch (result)
            {
                case ICreatePaymentOfSelleSuccessResultContract:
                    await context.RespondAsync<ICreatePaymentOfSelleSuccessResultContract>(result);
                    break;
                case ICreatePaymentOfSelleErrorResultContract:
                    await context.RespondAsync<ICreatePaymentOfSelleErrorResultContract>(result);
                    break;
            }
        }
    }
}