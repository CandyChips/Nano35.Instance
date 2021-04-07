using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllClientsTypes
{
    public class GetAllClientTypesConsumer : 
        IConsumer<IGetAllClientTypesRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public GetAllClientTypesConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<IGetAllClientTypesRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<IGetAllClientTypesRequestContract>) _services.GetService(typeof(ILogger<IGetAllClientTypesRequestContract>));
            
            var message = context.Message;
            
            var result =
                await new LoggedPipeNode<IGetAllClientTypesRequestContract, IGetAllClientTypesResultContract>(logger,
                    new ValidatedGetAllClientTypesRequest(
                        new GetAllClientTypesUseCase(dbContext))).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IGetAllClientTypesSuccessResultContract:
                    await context.RespondAsync<IGetAllClientTypesSuccessResultContract>(result);
                    break;
                case IGetAllClientTypesErrorResultContract:
                    await context.RespondAsync<IGetAllClientTypesErrorResultContract>(result);
                    break;
            }
        }
    }
}