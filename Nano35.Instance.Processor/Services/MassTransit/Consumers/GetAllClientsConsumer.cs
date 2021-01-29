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
    public class GetAllClientsConsumer : 
        IConsumer<IGetAllClientsRequestContract>
    {
        private readonly MediatR.IMediator _mediator;

        public GetAllClientsConsumer(
            MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllClientsRequestContract> context)
        {
            var result = await _mediator.Send(
                new GetAllClientsQuery(context.Message));
            
            if (result is IGetAllClientsSuccessResultContract)
            {
                await context.RespondAsync<IGetAllClientsSuccessResultContract>(result);
            }
            
            if (result is IGetAllClientsErrorResultContract)
            {
                await context.RespondAsync<IGetAllClientsErrorResultContract>(result);
            }
        }
    }
}