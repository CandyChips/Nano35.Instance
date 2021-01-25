using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Requests;

namespace Nano35.Instance.Processor.Services.MassTransit.Consumers
{
    public class CreateWorkerConsumer : 
        IConsumer<ICreateWorkerRequestContract>
    {
        private readonly MediatR.IMediator _mediator;
        
        public CreateWorkerConsumer(
            MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(
            ConsumeContext<ICreateWorkerRequestContract> context)
        {
            var result = await _mediator.Send(new CreateWorkerCommand(context.Message));
            await context.RespondAsync<ICreateWorkerSuccessResultContract>(result);
        }
    }
}