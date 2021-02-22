﻿using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests.CreateCashOutput;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Consumers
{
    public class CreateCashOutputConsumer : 
        IConsumer<ICreateCashOutputRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public CreateCashOutputConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<ICreateCashOutputRequestContract> context)
        {
            var dbContext = (ApplicationContext) _services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedCreateCashOutputRequest>) _services.GetService(typeof(ILogger<LoggedCreateCashOutputRequest>));

            var message = context.Message;
            
            var result =
                await new LoggedCreateCashOutputRequest(logger,
                        new ValidatedCreateCashOutputRequest(
                            new TransactedCreateCashOutputRequest(dbContext,
                                new CreateCashOutputRequest(dbContext))))
                    .Ask(message, context.CancellationToken);
            switch (result)
            {
                case ICreateCashOutputSuccessResultContract:
                    await context.RespondAsync<ICreateCashOutputSuccessResultContract>(result);
                    break;
                case ICreateCashOutputErrorResultContract:
                    await context.RespondAsync<ICreateCashOutputErrorResultContract>(result);
                    break;
            }
        }
    }
}