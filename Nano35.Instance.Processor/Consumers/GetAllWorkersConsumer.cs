using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests;

namespace Nano35.Instance.Processor.Consumers
{
    public class GetAllWorkersConsumer : 
        IConsumer<IGetAllWorkersRequestContract>
    {
        private readonly MediatR.IMediator _mediator;

        public GetAllWorkersConsumer(
            MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllWorkersRequestContract> context)
        {
            var message = context.Message;

            var request = new GetAllWorkersQuery(message);
            
            var result = await _mediator.Send(request);
            
            switch (result)
            {
                case IGetAllWorkersSuccessResultContract:
                    await context.RespondAsync<IGetAllWorkersSuccessResultContract>(result);
                    break;
                case IGetAllWorkersErrorResultContract:
                    await context.RespondAsync<IGetAllWorkersErrorResultContract>(result);
                    break;
            }
        }
    }
}