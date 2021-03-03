using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetUnitById
{
    public class GetUnitByIdConsumer : 
        IConsumer<IGetUnitByIdRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public GetUnitByIdConsumer(
            IServiceProvider services)
        {
            _services = services;
        }


        public async Task Consume(
            ConsumeContext<IGetUnitByIdRequestContract> context)
        {
            var dbcontect = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedGetUnitByIdRequest>) _services.GetService(typeof(ILogger<LoggedGetUnitByIdRequest>));
            
            var message = context.Message;
            
            var result =
                await new LoggedGetUnitByIdRequest(logger,
                    new ValidatedGetUnitByIdRequest(
                        new GetUnitByIdRequest(dbcontect))
                ).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IGetUnitByIdSuccessResultContract:
                    await context.RespondAsync<IGetUnitByIdSuccessResultContract>(result);
                    break;
                case IGetUnitByIdErrorResultContract:
                    await context.RespondAsync<IGetUnitByIdErrorResultContract>(result);
                    break;
            }
        }
    }
}