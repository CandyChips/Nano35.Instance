using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests;

namespace Nano35.Instance.Processor.Consumers
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
            var message = context.Message;
            
            var request = new CreateUnitCommand()
            {
                Id = message.Id,
                Name = message.Name,
                Adress = message.Adress,
                WorkingFormat = message.WorkingFormat,
                Phone = message.Phone,
                UnitTypeId = message.UnitTypeId,
                CreatorId = message.CreatorId,
                InstanceId = message.InstanceId
            };
            
            var result = await _mediator.Send(request);
            
            switch (result)
            {
                case ICreateUnitSuccessResultContract:
                    await context.RespondAsync<ICreateUnitSuccessResultContract>(result);
                    break;
                case ICreateUnitErrorResultContract:
                    await context.RespondAsync<ICreateUnitErrorResultContract>(result);
                    break;
            }
        }
    }
}