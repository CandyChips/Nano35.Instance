using System;
using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Consumers
{
    public class CreateCashInputConsumer : 
        IConsumer<ICreateCashInputRequestContract>
    {
        private readonly IServiceProvider  _services;
        
        public CreateCashInputConsumer(
            IServiceProvider services)
        {
            _services = services;
        }
        
        public async Task Consume(
            ConsumeContext<ICreateCashInputRequestContract> context)
        {
            throw new System.NotImplementedException();
        }
    }
}