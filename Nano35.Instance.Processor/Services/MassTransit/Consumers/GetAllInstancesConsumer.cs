using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.Services.MassTransit.Consumers
{
    public class GetAllInstancesConsumer : 
        IConsumer<IGetAllInstancesRequestContract>
    {
        private readonly ILogger<GetAllInstancesConsumer> _logger;
        private readonly ApplicationContext _context;
        
        public GetAllInstancesConsumer(
            ILogger<GetAllInstancesConsumer> logger, 
            ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }
        
        public async Task Consume(ConsumeContext<IGetAllInstancesRequestContract> context)
        {
            _logger.LogInformation("IGetAllInstancesRequestContract tracked");
            var message = context.Message;
            var result = await (this._context.Instances
                .Where(c => c.Deleted == false)
                .MapAllToAsync<IInstanceViewModel>());
            if (result.Count == 0)
            {
                await context.RespondAsync<IGetAllInstancesNotFoundResultContract>(new
                {
                });
            }
            else
            {
                await context.RespondAsync<IGetAllInstancesResultContract>(new
                {
                    Data = result
                });
            }
        }
    }
}