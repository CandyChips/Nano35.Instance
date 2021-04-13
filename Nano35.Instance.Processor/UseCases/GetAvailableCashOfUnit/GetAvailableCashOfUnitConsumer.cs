﻿using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAvailableCashOfUnit
{
    public class GetAvailableCashOfUnitConsumer :
        IConsumer<IGetAvailableCashOfUnitRequestContract>
    {
        private readonly IServiceProvider _services;

        public GetAvailableCashOfUnitConsumer(IServiceProvider services) { _services = services; }

        public async Task Consume(ConsumeContext<IGetAvailableCashOfUnitRequestContract> context)
        {
            var result = await new LoggedPipeNode<IGetAvailableCashOfUnitRequestContract, IGetAvailableCashOfUnitResultContract>(
                _services.GetService(typeof(ILogger<IGetAvailableCashOfUnitRequestContract>)) as ILogger<IGetAvailableCashOfUnitRequestContract>,
                new GetAvailableCashOfUnitUseCase(
                    _services.GetService(typeof(ApplicationContext)) as ApplicationContext))
                .Ask(context.Message, context.CancellationToken);
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