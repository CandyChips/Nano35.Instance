using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetInstanceById
{
    public class GetInstanceByIdConsumer : 
        IConsumer<IGetInstanceByIdRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public GetInstanceByIdConsumer(
            IServiceProvider services)
        {
            _services = services;
        }


        public async Task Consume(
            ConsumeContext<IGetInstanceByIdRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedGetInstanceByIdRequest>) _services.GetService(typeof(ILogger<LoggedGetInstanceByIdRequest>));
            
            var message = context.Message;
            
            var result =
                await new LoggedGetInstanceByIdRequest(logger,
                    new ValidatedGetInstanceByIdRequest(
                        new GetInstanceByIdRequest(dbContext))
                ).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IGetInstanceByIdSuccessResultContract:
                    await context.RespondAsync<IGetInstanceByIdSuccessResultContract>(result);
                    break;
                case IGetInstanceByIdErrorResultContract:
                    await context.RespondAsync<IGetInstanceByIdErrorResultContract>(result);
                    break;
            }
        }
    }
}