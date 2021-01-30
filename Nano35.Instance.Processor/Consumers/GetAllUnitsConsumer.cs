using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests;

namespace Nano35.Instance.Processor.Consumers
{
    public class GetAllUnitsConsumer : 
        IConsumer<IGetAllUnitsRequestContract>
    {
        private readonly MediatR.IMediator _mediator;

        public GetAllUnitsConsumer(
            MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllUnitsRequestContract> context)
        {
            var message = context.Message;

            var request = new GetAllUnitsQuery(message);
            
            var result = await _mediator.Send(request);
            
            switch (result)
            {
                case IGetAllUnitsSuccessResultContract:
                    await context.RespondAsync<IGetAllUnitsSuccessResultContract>(result);
                    break;
                case IGetAllUnitsErrorResultContract:
                    await context.RespondAsync<IGetAllUnitsErrorResultContract>(result);
                    break;
            }
        }
    }
}