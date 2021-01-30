using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests;

namespace Nano35.Instance.Processor.Consumers
{
    public class GetInstanceByIdConsumer : 
        IConsumer<IGetInstanceByIdRequestContract>
    {
        private readonly MediatR.IMediator _mediator;

        public GetInstanceByIdConsumer(
            IMediator mediator)
        {
            _mediator = mediator;
        }


        public async Task Consume(
            ConsumeContext<IGetInstanceByIdRequestContract> context)
        {
            var message = context.Message;

            var request = new GetInstanceByIdQuery(context.Message);
            
            var result = await _mediator.Send(request);
            
            switch (result)
            {
                case IGetInstanceByIdSuccessResultContract:
                    await context.RespondAsync<IGetInstanceByIdSuccessResultContract>(result);
                    break;
                case IGetInstanceByIdErrorResultContract:
                    await context.RespondAsync<IGetInstanceByIdErrorResultContract>(result);
                    break;
            }
        }
    }
}