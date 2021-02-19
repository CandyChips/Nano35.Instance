using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAvailableCashOfUnit
{
    public class GetAvailableCashOfUnitValidatorErrorResult : IGetAvailableCashOfUnitErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAvailableCashOfUnitRequest:
        IPipelineNode<IGetAvailableCashOfUnitRequestContract, IGetAvailableCashOfUnitResultContract>
    {
        private readonly IPipelineNode<IGetAvailableCashOfUnitRequestContract, IGetAvailableCashOfUnitResultContract> _nextNode;

        public ValidatedGetAvailableCashOfUnitRequest(
            IPipelineNode<IGetAvailableCashOfUnitRequestContract, IGetAvailableCashOfUnitResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAvailableCashOfUnitResultContract> Ask(
            IGetAvailableCashOfUnitRequestContract input)
        {
            if (false)
            {
                return new GetAvailableCashOfUnitValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}