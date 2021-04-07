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
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<IGetUnitByIdRequestContract>) _services.GetService(typeof(ILogger<IGetUnitByIdRequestContract>));
            
            var message = context.Message;
            
            var result =
                await new LoggedPipeNode<IGetUnitByIdRequestContract, IGetUnitByIdResultContract>(logger,
                    new ValidatedGetUnitByIdRequest(
                        new GetUnitByIdUseCase(dbContext))).Ask(message, context.CancellationToken);
            
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