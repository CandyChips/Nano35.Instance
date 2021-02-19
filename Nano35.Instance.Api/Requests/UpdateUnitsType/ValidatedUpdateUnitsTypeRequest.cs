using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateUnitsType
{
    public class UpdateUnitsTypeValidatorErrorResult : IUpdateUnitsTypeErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateUnitsTypeRequest:
        IPipelineNode<IUpdateUnitsTypeRequestContract, IUpdateUnitsTypeResultContract>
    {
        private readonly IPipelineNode<IUpdateUnitsTypeRequestContract, IUpdateUnitsTypeResultContract> _nextNode;

        public ValidatedUpdateUnitsTypeRequest(
            IPipelineNode<IUpdateUnitsTypeRequestContract, IUpdateUnitsTypeResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateUnitsTypeResultContract> Ask(
            IUpdateUnitsTypeRequestContract input)
        {
            if (false)
            {
                return new UpdateUnitsTypeValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}