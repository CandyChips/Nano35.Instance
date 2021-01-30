using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests;

namespace Nano35.Instance.Processor.Consumers
{
    public class GetAllUnitTypesConsumer : 
        IConsumer<IGetAllUnitTypesRequestContract>
    {
        private readonly MediatR.IMediator _mediator;

        public GetAllUnitTypesConsumer(
            MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllUnitTypesRequestContract> context)
        {
            var message = context.Message;

            var request = new GetAllUnitTypesQuery();
            
            var result = await _mediator.Send(request);
            
            switch (result)
            {
                case IGetAllUnitTypesSuccessResultContract:
                    await context.RespondAsync<IGetAllUnitTypesSuccessResultContract>(result);
                    break;
                case IGetAllUnitTypesErrorResultContract:
                    await context.RespondAsync<IGetAllUnitTypesErrorResultContract>(result);
                    break;
            }
        }
    }
}