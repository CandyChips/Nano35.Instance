using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.Services.MassTransit.Consumers
{
    public class GetInstanceByIdConsumer : 
        IConsumer<IGetInstanceByIdRequestContract>
    {
        private readonly ILogger<GetInstanceByIdConsumer> _logger;
        private readonly ApplicationContext _context;
        
        public GetInstanceByIdConsumer(
            ILogger<GetInstanceByIdConsumer> logger, 
            ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }
        
        public async Task Consume(ConsumeContext<IGetInstanceByIdRequestContract> context)
        {
            _logger.LogInformation("IGetInstanceByIdRequestContract tracked");
            var message = context.Message;
            var result = this._context.Instances
                .Find(message.InstanceId)
                .MapTo<IInstanceViewModel>();
            if (result == null)
            {
                await context.RespondAsync<IGetInstanceByIdNotFoundResultContract>(new
                {
                });
            }
            else
            {
                await context.RespondAsync<IGetInstanceByIdSuccessResultContract>(new
                {
                    Data = result
                });
            }
        }
    }
}