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
            var result = await new LoggedPipeNode<IGetWorkerStringsByIdsRequestContract, IGetWorkerStringsByIdsResultContract>(
                _services.GetService(typeof(ILogger<IGetWorkerStringsByIdsRequestContract>)) as ILogger<IGetWorkerStringsByIdsRequestContract>,
                    new GetWorkerStringsByIdsUseCase(dbContext))
                .Ask(context.Message, context.CancellationToken);
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