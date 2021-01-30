using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests;

namespace Nano35.Instance.Processor.Consumers
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
            var message = context.Message;
            
            var request = new CreateWorkerCommand()
            {
                NewId = message.NewId,
                InstanceId = message.InstanceId,
                RoleId = message.RoleId,
                Name = message.Name,
                Comment = message.Comment,
                Phone = message.Phone,
                Email = message.Email,
                Password = message.Password,
                PasswordConfirm = message.PasswordConfirm
            };
            
            var result = await _mediator.Send(request);
            
            switch (result)
            {
                case ICreateWorkerSuccessResultContract:
                    await context.RespondAsync<ICreateWorkerSuccessResultContract>(result);
                    break;
                case ICreateWorkerErrorResultContract:
                    await context.RespondAsync<ICreateWorkerErrorResultContract>(result);
                    break;
            }
        }
    }
}