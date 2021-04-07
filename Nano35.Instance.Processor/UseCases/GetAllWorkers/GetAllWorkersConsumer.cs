using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetAllWorkers
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
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<IGetAllWorkersRequestContract>) _services.GetService(typeof(ILogger<IGetAllWorkersRequestContract>));
            
            var message = context.Message;
            
            var result =
                await new LoggedPipeNode<IGetAllWorkersRequestContract, IGetAllWorkersResultContract>(logger,
                    new ValidatedGetAllWorkersRequest(
                        new GetAllWorkersUseCase(dbContext, bus))).Ask(message, context.CancellationToken);
            
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