using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests;

namespace Nano35.Instance.Processor.Consumers
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
            var message = context.Message;
            
            var request = new CreateClientCommand()
            {
                NewId = message.NewId,
                Name = message.Name,
                Email = message.Email,
                Phone = message.Phone,
                ClientStateId = message.ClientStateId,
                ClientTypeId = message.ClientTypeId,
                UserId = message.UserId,
                InstanceId = message.InstanceId
            };
            
            var result = await _mediator.Send(request);
            
            switch (result)
            {
                case ICreateClientSuccessResultContract:
                    await context.RespondAsync<ICreateClientSuccessResultContract>(result);
                    break;
                case ICreateClientErrorResultContract:
                    await context.RespondAsync<ICreateClientErrorResultContract>(result);
                    break;
            }
        }
    }
}