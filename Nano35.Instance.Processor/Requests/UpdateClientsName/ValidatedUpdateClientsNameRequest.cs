using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.UpdateClientsName
{
    public class UpdateClientsNameValidatorErrorResult : 
        IUpdateClientsNameErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateClientsNameRequest:
        IPipelineNode<IUpdateClientsNameRequestContract, IUpdateClientsNameResultContract>
    {
        private readonly IPipelineNode<IUpdateClientsNameRequestContract, IUpdateClientsNameResultContract> _nextNode;

        public ValidatedUpdateClientsNameRequest(
            IPipelineNode<IUpdateClientsNameRequestContract, IUpdateClientsNameResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateClientsNameResultContract> Ask(
            IUpdateClientsNameRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new UpdateClientsNameValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}