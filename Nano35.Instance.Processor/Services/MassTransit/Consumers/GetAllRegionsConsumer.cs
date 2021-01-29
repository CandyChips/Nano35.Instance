using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;
using Nano35.Instance.Processor.Services.Requests;

namespace Nano35.Instance.Processor.Services.MassTransit.Consumers
{
    public class GetAllRegionsConsumer : 
        IConsumer<IGetAllRegionsRequestContract>
    {
        private readonly MediatR.IMediator _mediator;

        public GetAllRegionsConsumer(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(
            ConsumeContext<IGetAllRegionsRequestContract> context)
        {
            var message = context.Message;
            
            var request = new GetAllRegionsQuery();
            
            var result = await _mediator.Send(request);
            
            switch (result)
            {
                case IGetAllRegionsSuccessResultContract:
                    await context.RespondAsync<IGetAllRegionsSuccessResultContract>(result);
                    break;
                case IGetAllRegionsErrorResultContract:
                    await context.RespondAsync<IGetAllRegionsErrorResultContract>(result);
                    break;
            }
        }
    }
}