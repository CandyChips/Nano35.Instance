using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllInstanceTypes
{
    public class GetAllInstanceTypesConsumer : 
        IConsumer<IGetAllInstanceTypesRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public GetAllInstanceTypesConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllInstanceTypesRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<LoggedGetAllInstanceTypesRequest>) _services.GetService(typeof(ILogger<LoggedGetAllInstanceTypesRequest>));
            
            var message = context.Message;
            
            var result =
                await new LoggedGetAllInstanceTypesRequest(logger,
                    new ValidatedGetAllInstanceTypesRequest(
                        new GetAllInstanceTypesUseCase(dbContext))).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IGetAllInstanceTypesSuccessResultContract:
                    await context.RespondAsync<IGetAllInstanceTypesSuccessResultContract>(result);
                    break;
                case IGetAllInstanceTypesErrorResultContract:
                    await context.RespondAsync<IGetAllInstanceTypesErrorResultContract>(result);
                    break;
            }
        }
    }
}