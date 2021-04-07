using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetWorkerStringById
{
    public class GetWorkerStringByIdConsumer : 
        IConsumer<IGetWorkerStringByIdRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public GetWorkerStringByIdConsumer(
            IServiceProvider services)
        {
            _services = services;
        }

        public async Task Consume(
            ConsumeContext<IGetWorkerStringByIdRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<IGetWorkerStringByIdRequestContract>) _services.GetService(typeof(ILogger<IGetWorkerStringByIdRequestContract>));
            var message = context.Message;
            
            var result =
                await new LoggedPipeNode<IGetWorkerStringByIdRequestContract, IGetWorkerStringByIdResultContract>(logger,
                    new ValidatedGetWorkerStringByIdRequest(
                        new GetWorkerStringByIdUseCase(dbContext))).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IGetWorkerStringByIdSuccessResultContract:
                    await context.RespondAsync<IGetWorkerStringByIdSuccessResultContract>(result);
                    break;
                case IGetWorkerStringByIdErrorResultContract:
                    await context.RespondAsync<IGetWorkerStringByIdErrorResultContract>(result);
                    break;
            }
        }
    }
}