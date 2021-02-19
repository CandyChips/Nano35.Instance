using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateClientsState
{
    public class UpdateClientsStateValidatorErrorResult : IUpdateClientsStateErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateClientsStateRequest:
        IPipelineNode<IUpdateClientsStateRequestContract, IUpdateClientsStateResultContract>
    {
        private readonly IPipelineNode<IUpdateClientsStateRequestContract, IUpdateClientsStateResultContract> _nextNode;

        public ValidatedUpdateClientsStateRequest(
            IPipelineNode<IUpdateClientsStateRequestContract, IUpdateClientsStateResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateClientsStateResultContract> Ask(
            IUpdateClientsStateRequestContract input)
        {
            if (false)
            {
                return new UpdateClientsStateValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}