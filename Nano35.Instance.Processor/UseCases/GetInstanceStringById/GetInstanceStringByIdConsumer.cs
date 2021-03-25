using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetInstanceStringById
{
    public class GetInstanceStringByIdConsumer : 
        IConsumer<IGetInstanceStringByIdRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public GetInstanceStringByIdConsumer(
            IServiceProvider services)
        {
            _services = services;
        }

        public async Task Consume(
            ConsumeContext<IGetInstanceStringByIdRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedGetInstanceStringByIdRequest>) _services.GetService(typeof(ILogger<LoggedGetInstanceStringByIdRequest>));
            
            var message = context.Message;
            
            var result =
                await new LoggedGetInstanceStringByIdRequest(logger,
                    new ValidatedGetInstanceStringByIdRequest(
                        new GetInstanceStringByIdRequest(dbContext))).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IGetInstanceStringByIdSuccessResultContract:
                    await context.RespondAsync<IGetInstanceStringByIdSuccessResultContract>(result);
                    break;
                case IGetInstanceStringByIdErrorResultContract:
                    await context.RespondAsync<IGetInstanceStringByIdErrorResultContract>(result);
                    break;
            }
        }
    }
}