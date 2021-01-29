using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Requests;

namespace Nano35.Instance.Processor.Services.MassTransit.Consumers
{
    public class GetAllInstancesConsumer : 
        IConsumer<IGetAllInstancesRequestContract>
    {
        private readonly MediatR.IMediator _mediator;

        public GetAllInstancesConsumer(
            MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllInstancesRequestContract> context)
        {
            var result = await _mediator.Send(
                new GetAllInstancesQuery(context.Message));
            if (result is IGetAllInstancesSuccessResultContract)
            {
                await context.RespondAsync<IGetAllInstancesSuccessResultContract>(result);
            }
            
            if (result is IGetAllInstancesErrorResultContract)
            {
                await context.RespondAsync<IGetAllInstancesErrorResultContract>(result);
            }
        }
    }
}