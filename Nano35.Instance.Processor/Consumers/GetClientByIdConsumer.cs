using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests.GetClientById;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Consumers
{
    public class GetClientByIdConsumer : 
        IConsumer<IGetClientByIdRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public GetClientByIdConsumer(
            IServiceProvider services)
        {
            _services = services;
        }


        public async Task Consume(
            ConsumeContext<IGetClientByIdRequestContract> context)
        {
            var dbcontect = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedGetClientByIdRequest>) _services.GetService(typeof(ILogger<LoggedGetClientByIdRequest>));
            
            var message = context.Message;
            
            var result =
                await new LoggedGetClientByIdRequest(logger,
                    new ValidatedGetClientByIdRequest(
                        new GetClientByIdRequest(dbcontect))
                ).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IGetClientByIdSuccessResultContract:
                    await context.RespondAsync<IGetClientByIdSuccessResultContract>(result);
                    break;
                case IGetClientByIdErrorResultContract:
                    await context.RespondAsync<IGetClientByIdErrorResultContract>(result);
                    break;
            }
        }
    }
}