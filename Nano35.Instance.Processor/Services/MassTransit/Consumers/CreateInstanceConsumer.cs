using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.Requests;

namespace Nano35.Instance.Processor.Services.MassTransit.Consumers
{
    public class CreateInstanceConsumer : 
        IConsumer<ICreateInstanceRequestContract>
    {
        private readonly MediatR.IMediator _mediator;
        public CreateInstanceConsumer(
            MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(ConsumeContext<ICreateInstanceRequestContract> context)
        {
            await context.RespondAsync<ICreateInstanceSuccessResultContract>(
                await _mediator.Send(new CreateInstanceCommand(context.Message)));
        }
    }
}