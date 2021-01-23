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
    public class GetAllInstancesConsumer : 
        IConsumer<IGetAllInstancesRequestContract>
    {
        private readonly MediatR.IMediator _mediator;

        public GetAllInstancesConsumer(
            MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllInstancesRequestContract> context)
        {
            await context.RespondAsync<IGetAllInstancesSuccessResultContract>(
                await _mediator.Send(new GetAllInstancesQuery(context.Message)));
        }
    }
}