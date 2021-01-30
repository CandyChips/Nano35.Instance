using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests;

namespace Nano35.Instance.Processor.Consumers
{
    public class GetAllClientStatesConsumer : 
        IConsumer<IGetAllClientStatesRequestContract>
    {
        private readonly MediatR.IMediator _mediator;

        public GetAllClientStatesConsumer(
            MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllClientStatesRequestContract> context)
        {
            var result = await _mediator.Send(
                new GetAllClientStatesQuery());
            
            switch (result)
            {
                case IGetAllClientStatesSuccessResultContract:
                    await context.RespondAsync<IGetAllClientStatesSuccessResultContract>(result);
                    break;
                case IGetAllClientStatesErrorResultContract:
                    await context.RespondAsync<IGetAllClientStatesErrorResultContract>(result);
                    break;
            }
        }
    }
}