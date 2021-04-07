using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllUnitTypes
{
    public class GetAllUnitTypesConsumer : 
        IConsumer<IGetAllUnitTypesRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public GetAllUnitTypesConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllUnitTypesRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<IGetAllUnitTypesRequestContract>) _services.GetService(typeof(ILogger<IGetAllUnitTypesRequestContract>));
            
            var message = context.Message;
            
            var result =
                await new LoggedPipeNode<IGetAllUnitTypesRequestContract, IGetAllUnitTypesResultContract>(logger,
                    new ValidateGetAllUnitTypesRequest(
                        new GetAllUnitTypesUseCase(dbContext))).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IGetAllUnitTypesSuccessResultContract:
                    await context.RespondAsync<IGetAllUnitTypesSuccessResultContract>(result);
                    break;
                case IGetAllUnitTypesErrorResultContract:
                    await context.RespondAsync<IGetAllUnitTypesErrorResultContract>(result);
                    break;
            }
        }
    }
}