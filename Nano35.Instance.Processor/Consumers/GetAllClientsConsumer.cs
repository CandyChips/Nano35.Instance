using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests;

namespace Nano35.Instance.Processor.Consumers
{
    public class GetAllClientsConsumer : 
        IConsumer<IGetAllClientsRequestContract>
    {
        private readonly MediatR.IMediator _mediator;

        public GetAllClientsConsumer(
            MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllClientsRequestContract> context)
        {
            var message = context.Message;
            
            var request = new GetAllClientsQuery()
            {
                ClientStateId = message.ClientStateId,
                ClientTypeId = message.ClientTypeId,
                InstanceId = message.InstanceId
            };
            
            var result = await _mediator.Send(request);
            
            switch (result)
            {
                case IGetAllClientsSuccessResultContract:
                    await context.RespondAsync<IGetAllClientsSuccessResultContract>(result);
                    break;
                case IGetAllClientsErrorResultContract:
                    await context.RespondAsync<IGetAllClientsErrorResultContract>(result);
                    break;
            }
        }
    }
}