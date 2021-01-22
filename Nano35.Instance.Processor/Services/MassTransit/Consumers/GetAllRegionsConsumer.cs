using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Contracts.Instance.Models;
using Nano35.Instance.Processor.Services.Contexts;
using Nano35.Instance.Processor.Services.MappingProfiles;

namespace Nano35.Instance.Processor.Services.MassTransit.Consumers
{
    public class GetAllRegionsConsumer : 
        IConsumer<IGetAllRegionsRequestContract>
    {
        private readonly ILogger<GetAllRegionsConsumer> _logger;
        private readonly ApplicationContext _context;
        
        public GetAllRegionsConsumer(
            ILogger<GetAllRegionsConsumer> logger, 
            ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }
        
        public async Task Consume(ConsumeContext<IGetAllRegionsRequestContract> context)
        {
            _logger.LogInformation("IGetAllInstancesRequestContract tracked");
            var message = context.Message;
            var result = await this._context.Regions
                .MapAllToAsync<IRegionViewModel>();
            if (result.Count == 0)
            {
                await context.RespondAsync<IGetAllRegionsNotFoundResultContract>(new
                {
                });
            }
            else
            {
                await context.RespondAsync<IGetAllRegionsResultContract>(new
                {
                    Data = result
                });
            }
        }
    }
}