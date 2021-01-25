using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
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
            var result = await _mediator.Send(new CreateInstanceCommand(context.Message));
            await context.RespondAsync<ICreateInstanceSuccessResultContract>(result);
        }
    }
}