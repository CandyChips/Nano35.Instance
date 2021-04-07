using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetWorkerById
{
    public class GetWorkerByIdConsumer : 
        IConsumer<IGetWorkerByIdRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public GetWorkerByIdConsumer(
            IServiceProvider services)
        {
            _services = services;
        }


        public async Task Consume(
            ConsumeContext<IGetWorkerByIdRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var logger = (ILogger<IGetWorkerByIdRequestContract>) _services.GetService(typeof(ILogger<IGetWorkerByIdRequestContract>));
            
            var result =
                await new LoggedPipeNode<IGetWorkerByIdRequestContract, IGetWorkerByIdResultContract>(logger,
                    new ValidatedGetWorkerByIdRequest(
                        new GetWorkerByIdUseCase(dbContext))).Ask(context.Message, context.CancellationToken);
            
            switch (result)
            {
                case IGetWorkerByIdSuccessResultContract:
                    await context.RespondAsync<IGetWorkerByIdSuccessResultContract>(result);
                    break;
                case IGetWorkerByIdErrorResultContract:
                    await context.RespondAsync<IGetWorkerByIdErrorResultContract>(result);
                    break;
            }
        }
    }
}