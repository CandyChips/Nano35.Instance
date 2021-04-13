using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.CreatePaymentOfComing
{
    public class CreatePaymentOfComingConsumer :
        IConsumer<ICreatePaymentOfComingRequestContract>
    {
        private readonly IServiceProvider _services;

        public CreatePaymentOfComingConsumer(
            IServiceProvider services)
        {
            _services = services;
        }

        public async Task Consume(
            ConsumeContext<ICreatePaymentOfComingRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger =
                (ILogger<ICreatePaymentOfComingRequestContract>) _services.GetService(typeof(ILogger<ICreatePaymentOfComingRequestContract>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedPipeNode<ICreatePaymentOfComingRequestContract, ICreatePaymentOfComingResultContract>(logger,
                        new TransactedPipeNode<ICreatePaymentOfComingRequestContract, ICreatePaymentOfComingResultContract>(dbContext,
                            new CreatePaymentOfComingUseCase(dbContext))).Ask(message, context.CancellationToken);

            // Check response of create client request
            switch (result)
            {
                case ICreatePaymentOfComingSuccessResultContract:
                    await context.RespondAsync<ICreatePaymentOfComingSuccessResultContract>(result);
                    break;
                case ICreatePaymentOfComingErrorResultContract:
                    await context.RespondAsync<ICreatePaymentOfComingErrorResultContract>(result);
                    break;
            }
        }
    }
}