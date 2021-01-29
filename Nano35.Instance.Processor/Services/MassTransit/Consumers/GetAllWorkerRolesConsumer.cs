using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;
using Nano35.Instance.Processor.Services.Requests;

namespace Nano35.Instance.Processor.Services.MassTransit.Consumers
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
            var result = await _mediator.Send(
                new GetAllWorkerRolesQuery(context.Message));
            
            if (result is IGetAllWorkerRolesSuccessResultContract)
            {
                await context.RespondAsync<IGetAllWorkerRolesSuccessResultContract>(result);
            }
            
            if (result is IGetAllWorkerRolesErrorResultContract)
            {
                await context.RespondAsync<IGetAllWorkerRolesErrorResultContract>(result);
            }
        }
    }
}