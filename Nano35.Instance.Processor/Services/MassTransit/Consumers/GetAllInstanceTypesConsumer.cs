using System.Threading.Tasks;
using MassTransit;
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
        private readonly ILogger<GetAllInstanceTypesConsumer> _logger;
        private readonly ApplicationContext _context;
        
        public GetAllInstanceTypesConsumer(
            ILogger<GetAllInstanceTypesConsumer> logger, 
            ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }
        
        public async Task Consume(ConsumeContext<IGetAllInstanceTypesRequestContract> context)
        {
            _logger.LogInformation("IGetAllInstancesRequestContract tracked");
            var message = context.Message;
            var result = await this._context.InstanceTypes
                .MapAllToAsync<IInstanceTypeViewModel>();
            if (result.Count == 0)
            {
                await context.RespondAsync<IGetAllInstanceTypesNotFoundResultContract>(new
                {
                });
            }
            else
            {
                await context.RespondAsync<IGetAllInstanceTypesResultContract>(new
                {
                    Data = result
                });
            }
        }
    }
}