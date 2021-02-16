using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Consumers
{
    public class GetAvailableCashOfUnitConsumer : 
        IConsumer<IGetAvailableCashOfUnitRequestContract>
    {
        public async Task Consume(ConsumeContext<IGetAvailableCashOfUnitRequestContract> context)
        {
            throw new System.NotImplementedException();
        }
    }
}