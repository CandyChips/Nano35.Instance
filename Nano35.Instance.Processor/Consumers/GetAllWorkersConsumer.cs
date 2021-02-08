using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests;
using Nano35.Instance.Processor.Requests.GetAllWorkers;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Consumers
{
    public class GetAllWorkersConsumer : 
        IConsumer<IGetAllWorkersRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public GetAllWorkersConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllWorkersRequestContract> context)
        {
            var dbcontect = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<GetAllWorkersLogger>) _services.GetService(typeof(ILogger<GetAllWorkersLogger>));
            
            var message = context.Message;
            
            var result =
                await new GetAllWorkersLogger(logger,
                    new GetAllWorkersValidator(
                        new GetAllWorkersRequest(dbcontect, bus))
                ).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IGetAllWorkersSuccessResultContract:
                    await context.RespondAsync<IGetAllWorkersSuccessResultContract>(result);
                    break;
                case IGetAllWorkersErrorResultContract:
                    await context.RespondAsync<IGetAllWorkersErrorResultContract>(result);
                    break;
            }
        }
    }
}