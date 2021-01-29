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
    public class GetAllUnitsConsumer : 
        IConsumer<IGetAllUnitsRequestContract>
    {
        private readonly MediatR.IMediator _mediator;

        public GetAllUnitsConsumer(
            MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllUnitsRequestContract> context)
        {
            var result = await _mediator.Send(
                new GetAllUnitsQuery(context.Message));
            
            if (result is IGetAllUnitsSuccessResultContract)
            {
                await context.RespondAsync<IGetAllUnitsSuccessResultContract>(result);
            }
            
            if (result is IGetAllUnitsErrorResultContract)
            {
                await context.RespondAsync<IGetAllUnitsErrorResultContract>(result);
            }
        }
    }
}