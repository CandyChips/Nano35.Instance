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
            var logger = (ILogger<IGetInstanceByIdRequestContract>) _services.GetService(typeof(ILogger<IGetInstanceByIdRequestContract>));
            
            var message = context.Message;
            
            var result =
                await new LoggedPipeNode<IGetInstanceByIdRequestContract, IGetInstanceByIdResultContract>(logger,
                    new ValidatedGetInstanceByIdRequest(
                        new GetInstanceByIdUseCase(dbContext))).Ask(message, context.CancellationToken);
            
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