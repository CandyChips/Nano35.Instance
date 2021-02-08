using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests;
using Nano35.Instance.Processor.Requests.GetAllInstances;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Consumers
{
    public class GetAllInstancesConsumer : 
        IConsumer<IGetAllInstancesRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public GetAllInstancesConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllInstancesRequestContract> context)
        {
            var dbcontect = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<GetAllInstancesLogger>) _services.GetService(typeof(ILogger<GetAllInstancesLogger>));
            
            var message = context.Message;
            
            var result =
                await new GetAllInstancesLogger(logger,
                    new GetAllInstancesValidator(
                        new GetAllInstancesRequest(dbcontect))
                ).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IGetAllInstancesSuccessResultContract:
                    await context.RespondAsync<IGetAllInstancesSuccessResultContract>(result);
                    break;
                case IGetAllInstancesErrorResultContract:
                    await context.RespondAsync<IGetAllInstancesErrorResultContract>(result);
                    break;
            }
        }
    }
}