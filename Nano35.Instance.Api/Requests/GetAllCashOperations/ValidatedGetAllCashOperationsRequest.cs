using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllCashOperations
{
    public class GetAllCashOperationsValidatorErrorResult : IGetAllCashOperationsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllCashOperationsRequest:
        IPipelineNode<IGetAllCashOperationsRequestContract, IGetAllCashOperationsResultContract>
    {
        private readonly IPipelineNode<IGetAllCashOperationsRequestContract, IGetAllCashOperationsResultContract> _nextNode;

        public ValidatedGetAllCashOperationsRequest(
            IPipelineNode<IGetAllCashOperationsRequestContract, IGetAllCashOperationsResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllCashOperationsResultContract> Ask(
            IGetAllCashOperationsRequestContract input)
        {
            if (false)
            {
                return new GetAllCashOperationsValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}