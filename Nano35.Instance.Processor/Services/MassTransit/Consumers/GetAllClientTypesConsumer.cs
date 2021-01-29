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
            var result = await _mediator.Send(
                new GetAllClientTypesQuery(context.Message));
            
            if (result is IGetAllClientTypesSuccessResultContract)
            {
                await context.RespondAsync<IGetAllClientTypesSuccessResultContract>(result);
            }
            
            if (result is IGetAllClientTypesErrorResultContract)
            {
                await context.RespondAsync<IGetAllClientTypesErrorResultContract>(result);
            }
        }
    }
}