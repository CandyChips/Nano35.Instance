using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Requests;

namespace Nano35.Instance.Processor.Services.MassTransit.Consumers
{
    public class CreateUnitConsumer : 
        IConsumer<ICreateUnitRequestContract>
    {
        private readonly MediatR.IMediator _mediator;
        
        public CreateUnitConsumer(
            MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(
            ConsumeContext<ICreateUnitRequestContract> context)
        {
            var result = await _mediator.Send(new CreateUnitCommand(context.Message));
            await context.RespondAsync<ICreateUnitSuccessResultContract>(result);
        }
    }
}