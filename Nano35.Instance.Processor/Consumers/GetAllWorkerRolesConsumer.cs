using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests;

namespace Nano35.Instance.Processor.Consumers
{
    public class GetAllWorkerRolesConsumer : 
        IConsumer<IGetAllWorkerRolesRequestContract>
    {
        private readonly MediatR.IMediator _mediator;

        public GetAllWorkerRolesConsumer(
            MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllWorkerRolesRequestContract> context)
        {
            var message = context.Message;
            
            var request = new GetAllWorkerRolesQuery();
            
            var result = await _mediator.Send(request);
            
            switch (result)
            {
                case IGetAllWorkerRolesSuccessResultContract:
                    await context.RespondAsync<IGetAllWorkerRolesSuccessResultContract>(result);
                    break;
                case IGetAllWorkerRolesErrorResultContract:
                    await context.RespondAsync<IGetAllWorkerRolesErrorResultContract>(result);
                    break;
            }
        }
    }
}