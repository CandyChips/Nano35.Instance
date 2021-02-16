using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Consumers
{
    public class CreatePaymentOfSelleConsumer : 
        IConsumer<ICreatePaymentOfSelleRequestContract>
    {
        public async Task Consume(ConsumeContext<ICreatePaymentOfSelleRequestContract> context)
        {
            throw new System.NotImplementedException();
        }
    }
}