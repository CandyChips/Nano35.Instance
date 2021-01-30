using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests;

namespace Nano35.Instance.Processor.Consumers
{
    public class GetAllInstanceTypesConsumer : 
        IConsumer<IGetAllInstanceTypesRequestContract>
    {
        private readonly MediatR.IMediator _mediator;

        public GetAllInstanceTypesConsumer(
            IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllInstanceTypesRequestContract> context)
        {
            var message = context.Message;

            var request = new GetAllInstanceTypesQuery();
            
            var result = await _mediator.Send(request);
            
            switch (result)
            {
                case IGetAllInstanceTypesSuccessResultContract:
                    await context.RespondAsync<IGetAllInstanceTypesSuccessResultContract>(result);
                    break;
                case IGetAllInstanceTypesErrorResultContract:
                    await context.RespondAsync<IGetAllInstanceTypesErrorResultContract>(result);
                    break;
            }
        }
    }
}