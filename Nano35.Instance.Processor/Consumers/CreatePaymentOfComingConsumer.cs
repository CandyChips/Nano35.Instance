using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Consumers
{
    public class CreatePaymentOfComingConsumer : 
        IConsumer<ICreatePaymentOfComingRequestContract>
    {
        public async Task Consume(ConsumeContext<ICreatePaymentOfComingRequestContract> context)
        {
            throw new System.NotImplementedException();
        }
    }
}