using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Requests;

namespace Nano35.Instance.Processor.Services.MassTransit.Consumers
{
    public class CreateInstanceConsumer : 
        IConsumer<ICreateInstanceRequestContract>
    {
        private readonly MediatR.IMediator _mediator;
        public CreateInstanceConsumer(
            MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Consume(ConsumeContext<ICreateInstanceRequestContract> context)
        {
            var message = context.Message;
            var request = new CreateInstanceCommand()
            {
                NewId = message.NewId,
                UserId = message.UserId,
                Name = message.Name,
                RealName = message.RealName,
                Email = message.Email,
                Info = message.Info,
                Phone = message.Phone,
                TypeId = message.TypeId,
                RegionId = message.RegionId
            };
            var result = await _mediator.Send(request);
            
            switch (result)
            {
                case ICreateInstanceSuccessResultContract:
                    await context.RespondAsync<ICreateInstanceSuccessResultContract>(result);
                    break;
                case ICreateInstanceErrorResultContract:
                    await context.RespondAsync<ICreateInstanceErrorResultContract>(result);
                    break;
            }
        }
    }
}