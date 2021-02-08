using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Processor.Requests;
using Nano35.Instance.Processor.Requests.CreateClient;
using Nano35.Instance.Processor.Requests.CreateInstance;
using Nano35.Instance.Processor.Services.Contexts;

namespace Nano35.Instance.Processor.Consumers
{
    public class CreateInstanceConsumer : 
        IConsumer<ICreateInstanceRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public CreateInstanceConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        public async Task Consume(
            ConsumeContext<ICreateInstanceRequestContract> context)
        {
            var dbcontect = (ApplicationContext)_services.GetService(typeof(ApplicationContext));
            var logger = (ILogger<CreateInstanceLogger>)_services.GetService(typeof(ILogger<CreateInstanceLogger>));
            
            var message = context.Message;
            
            var result =
                await new CreateInstanceLogger(logger,
                    new CreateInstanceValidator(
                        new CreateInstanceTransaction(dbcontect,
                            new CreateInstanceRequest(dbcontect)))
                ).Ask(message, context.CancellationToken);
            
            switch (result)
            {
                case ICreateInstanceSuccessResultContract:
                    await context.RespondAsync<ICreateInstanceSuccessResultContract>(result);
                    break;
                case ICreateInstanceErrorResultContract:
                    await context.RespondAsync<ICreateInstanceErrorResultContract>(result);
                    break;
            }
        }
    }
}