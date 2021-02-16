using System.Threading.Tasks;
using MassTransit;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Consumers
{
    public class GetAllCashOperationsConsumer : 
        IConsumer<IGetAllCashOperationsRequestContract>
    {
        public async Task Consume(ConsumeContext<IGetAllCashOperationsRequestContract> context)
        {
            throw new System.NotImplementedException();
        }
    }
}