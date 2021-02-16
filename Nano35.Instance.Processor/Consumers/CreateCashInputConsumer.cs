using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Consumers
{
    public class CreateCashInputConsumer : 
        IConsumer<ICreateCashInputRequestContract>
    {
        public async Task Consume(ConsumeContext<ICreateCashInputRequestContract> context)
        {
            throw new System.NotImplementedException();
        }
    }
}