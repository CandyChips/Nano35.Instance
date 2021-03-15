using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllCashOperations
{
    public class ValidatedGetAllCashOperationsRequest:
        PipeNodeBase<IGetAllCashOperationsRequestContract, IGetAllCashOperationsResultContract>
    {
        public ValidatedGetAllCashOperationsRequest(
            IPipeNode<IGetAllCashOperationsRequestContract, IGetAllCashOperationsResultContract> next) :
            base(next) {}

        public override async Task<IGetAllCashOperationsResultContract> Ask(
            IGetAllCashOperationsRequestContract input)
        {
            return await DoNext(input);
        }
    }
}