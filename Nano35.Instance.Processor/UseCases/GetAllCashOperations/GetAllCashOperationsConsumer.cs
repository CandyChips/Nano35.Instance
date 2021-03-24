using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllCashOperations
{
    public class GetAllCashOperationsConsumer :
        IConsumer<IGetAllCashOperationsRequestContract>
    {
        private readonly IServiceProvider _services;

        public GetAllCashOperationsConsumer(
            IServiceProvider services)
        {
            _services = services;
        }

        public async Task Consume(
            ConsumeContext<IGetAllCashOperationsRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger =
                (ILogger<LoggedGetAllCashOperationsRequest>) _services.GetService(
                    typeof(ILogger<LoggedGetAllCashOperationsRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedGetAllCashOperationsRequest(logger,
                    new ValidatedGetAllCashOperationsRequest(
                        new GetAllCashOperationsRequest(dbContext))).Ask(message, context.CancellationToken);

            // Check response of create client request
            switch (result)
            {
                case IGetAllCashOperationsSuccessResultContract:
                    await context.RespondAsync<IGetAllCashOperationsSuccessResultContract>(result);
                    break;
                case IGetAllCashOperationsErrorResultContract:
                    await context.RespondAsync<IGetAllCashOperationsErrorResultContract>(result);
                    break;
            }
        }
    }
}