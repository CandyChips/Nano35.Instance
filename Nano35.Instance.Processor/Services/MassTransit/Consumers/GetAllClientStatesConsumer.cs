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
                new GetAllClientStatesQuery(context.Message));
            
            if (result is IGetAllClientStatesSuccessResultContract)
            {
                await context.RespondAsync<IGetAllClientStatesSuccessResultContract>(result);
            }
            
            if (result is IGetAllClientStatesErrorResultContract)
            {
                await context.RespondAsync<IGetAllClientStatesErrorResultContract>(result);
            }
        }
    }
}