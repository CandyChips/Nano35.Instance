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
            var message = context.Message;
            
            var request = new GetAllClientsQuery()
            {
                ClientStateId = message.ClientStateId,
                ClientTypeId = message.ClientTypeId,
                InstanceId = message.InstanceId
            };
            
            var result = await _mediator.Send(request);
            
            switch (result)
            {
                case IGetAllClientsSuccessResultContract:
                    await context.RespondAsync<IGetAllClientsSuccessResultContract>(result);
                    break;
                case IGetAllClientsErrorResultContract:
                    await context.RespondAsync<IGetAllClientsErrorResultContract>(result);
                    break;
            }
        }
    }
}