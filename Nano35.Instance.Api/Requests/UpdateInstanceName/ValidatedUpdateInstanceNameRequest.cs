using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateInstanceName
{
    public class UpdateInstanceNameValidatorErrorResult : IUpdateInstanceNameErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateInstanceNameRequest:
        IPipelineNode<IUpdateInstanceNameRequestContract, IUpdateInstanceNameResultContract>
    {
        private readonly IPipelineNode<IUpdateInstanceNameRequestContract, IUpdateInstanceNameResultContract> _nextNode;

        public ValidatedUpdateInstanceNameRequest(
            IPipelineNode<IUpdateInstanceNameRequestContract, IUpdateInstanceNameResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateInstanceNameResultContract> Ask(
            IUpdateInstanceNameRequestContract input)
        {
            if (false)
            {
                return new UpdateInstanceNameValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}