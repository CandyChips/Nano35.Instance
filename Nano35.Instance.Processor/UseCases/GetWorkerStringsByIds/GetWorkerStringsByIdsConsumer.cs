using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.UseCases.GetWorkerStringsByIds
{
    public class GetWorkerStringsByIdsConsumer : 
        IConsumer<IGetWorkerStringsByIdsRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public GetWorkerStringsByIdsConsumer(
            IServiceProvider services)
        {
            _services = services;
        }

        public async Task Consume(
            ConsumeContext<IGetWorkerStringsByIdsRequestContract> context)
        {
            var dbContext = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<IGetWorkerStringsByIdsRequestContract>) _services.GetService(typeof(ILogger<IGetWorkerStringsByIdsRequestContract>));
            var message = context.Message;
            
            var result =
                await new LoggedPipeNode<IGetWorkerStringsByIdsRequestContract, IGetWorkerStringsByIdsResultContract>(logger,
                        new GetWorkerStringsByIdsUseCase(dbContext)).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case IGetWorkerStringsByIdsSuccessResultContract:
                    await context.RespondAsync<IGetWorkerStringsByIdsSuccessResultContract>(result);
                    break;
                case IGetWorkerStringsByIdsErrorResultContract:
                    await context.RespondAsync<IGetWorkerStringsByIdsErrorResultContract>(result);
                    break;
            }
        }
    }
}