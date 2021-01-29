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
    public class GetAllInstanceTypesConsumer : 
        IConsumer<IGetAllInstanceTypesRequestContract>
    {
        private readonly MediatR.IMediator _mediator;

        public GetAllInstanceTypesConsumer(
            IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllInstanceTypesRequestContract> context)
        {
            var message = context.Message;

            var request = new GetAllInstanceTypesQuery();
            
            var result = await _mediator.Send(request);
            
            switch (result)
            {
                case IGetAllInstanceTypesSuccessResultContract:
                    await context.RespondAsync<IGetAllInstanceTypesSuccessResultContract>(result);
                    break;
                case IGetAllInstanceTypesErrorResultContract:
                    await context.RespondAsync<IGetAllInstanceTypesErrorResultContract>(result);
                    break;
            }
        }
    }
}