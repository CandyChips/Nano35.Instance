using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests;

namespace Nano35.Instance.Processor.Consumers
{
    public class GetAllClientTypesConsumer : 
        IConsumer<IGetAllClientTypesRequestContract>
    {
        private readonly MediatR.IMediator _mediator;

        public GetAllClientTypesConsumer(
            MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllClientTypesRequestContract> context)
        {
            var message = context.Message;

            var request = new GetAllClientTypesQuery();
            
            var result = await _mediator.Send(request);
            
            switch (result)
            {
                case IGetAllClientTypesSuccessResultContract:
                    await context.RespondAsync<IGetAllClientTypesSuccessResultContract>(result);
                    break;
                case IGetAllClientTypesErrorResultContract:
                    await context.RespondAsync<IGetAllClientTypesErrorResultContract>(result);
                    break;
            }
        }
    }
}