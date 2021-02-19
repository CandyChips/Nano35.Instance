using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateClientsType
{
    public class UpdateClientsTypeValidatorErrorResult : IUpdateClientsTypeErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateClientsTypeRequest:
        IPipelineNode<IUpdateClientsTypeRequestContract, IUpdateClientsTypeResultContract>
    {
        private readonly IPipelineNode<IUpdateClientsTypeRequestContract, IUpdateClientsTypeResultContract> _nextNode;

        public ValidatedUpdateClientsTypeRequest(
            IPipelineNode<IUpdateClientsTypeRequestContract, IUpdateClientsTypeResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateClientsTypeResultContract> Ask(
            IUpdateClientsTypeRequestContract input)
        {
            if (false)
            {
                return new UpdateClientsTypeValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}