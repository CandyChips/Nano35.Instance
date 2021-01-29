using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Requests;

namespace Nano35.Instance.Processor.Services.MassTransit.Consumers
{
    public class CreateClientConsumer : 
        IConsumer<ICreateClientRequestContract>
    {
        private readonly MediatR.IMediator _mediator;
        
        public CreateClientConsumer(
            MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(
            ConsumeContext<ICreateClientRequestContract> context)
        {
            var result = await _mediator.Send(new CreateClientCommand(context.Message));
            await context.RespondAsync<ICreateClientSuccessResultContract>(result);
        }
    }
}