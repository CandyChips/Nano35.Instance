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
            var result = await _mediator.Send(new GetAllRegionsQuery(context.Message));
            if (result is IGetAllRegionsSuccessResultContract)
            {
                await context.RespondAsync<IGetAllRegionsSuccessResultContract>(result);
            }
            if (result is IGetAllRegionsErrorResultContract)
            {
                if (result is IGetAllRegionsNotFoundResultContract)
                {
                    await context.RespondAsync<IGetAllRegionsNotFoundResultContract>(result);
                }
            }
        }
    }
}