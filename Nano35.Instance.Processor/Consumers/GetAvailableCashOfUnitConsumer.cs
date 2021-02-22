﻿using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests.GetAvailableCashOfUnit;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Consumers
{
    public class GetAvailableCashOfUnitConsumer :
        IConsumer<IGetAvailableCashOfUnitRequestContract>
    {
        private readonly IServiceProvider _services;

        public GetAvailableCashOfUnitConsumer(
            IServiceProvider services)
        {
            _services = services;
        }

        public async Task Consume(
            ConsumeContext<IGetAvailableCashOfUnitRequestContract> context)
        {
            // Setup configuration of pipeline
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger =
                (ILogger<LoggedGetAvailableCashOfUnitRequest>) _services.GetService(
                    typeof(ILogger<LoggedGetAvailableCashOfUnitRequest>));

            // Explore message of request
            var message = context.Message;

            // Send request to pipeline
            var result =
                await new LoggedGetAvailableCashOfUnitRequest(logger,
                    new ValidatedGetAvailableCashOfUnitRequest(
                        new GetAvailableCashOfUnitRequest(dbContext))
                ).Ask(message, context.CancellationToken);

            // Check response of create client request
            switch (result)
            {
                case IGetAvailableCashOfUnitSuccessResultContract:
                    await context.RespondAsync<IGetAvailableCashOfUnitSuccessResultContract>(result);
                    break;
                case IGetAvailableCashOfUnitErrorResultContract:
                    await context.RespondAsync<IGetAvailableCashOfUnitErrorResultContract>(result);
                    break;
            }
        }
    }
}