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
    public class GetInstanceByIdConsumer : 
        IConsumer<IGetInstanceByIdRequestContract>
    {
        private readonly MediatR.IMediator _mediator;

        public GetInstanceByIdConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }


        public async Task Consume(ConsumeContext<IGetInstanceByIdRequestContract> context)
        {
            var result = await _mediator.Send(new GetInstanceByIdQuery(context.Message));
            if (result is IGetInstanceByIdSuccessResultContract)
            {
                await context.RespondAsync<IGetInstanceByIdSuccessResultContract>(result);
            }
            if (result is IGetInstanceByIdErrorResultContract)
            {
                if (result is IGetInstanceByIdNotFoundResultContract)
                {
                    await context.RespondAsync<IGetInstanceByIdNotFoundResultContract>(result);
                }
            }
        }
    }
}