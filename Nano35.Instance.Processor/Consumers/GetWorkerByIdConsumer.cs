using System;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests;
using Nano35.Instance.Processor.Requests.GetWorkerById;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Consumers
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
            var dbcontect = (ApplicationContext)_services.GetService(typeof(ApplicationContext)); 
            var logger = (ILogger<LoggedGetWorkerByIdRequest>) _services.GetService(typeof(ILogger<LoggedGetWorkerByIdRequest>));
            
            var message = context.Message;
            
            var result =
                await new LoggedGetWorkerByIdRequest(logger,
                    new ValidatedGetWorkerByIdRequest(
                        new GetWorkerByIdRequest(dbcontect))
                ).Ask(message, context.CancellationToken);
            
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