using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.Services.MassTransit.Consumers
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
        
        public async Task Consume(ConsumeContext<IGetAllInstanceTypesRequestContract> context)
        {
            await context.RespondAsync<IGetAllInstanceTypesSuccessResultContract>(_mediator.Send(context.Message));
        }
    }
}