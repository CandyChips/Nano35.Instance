using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.UpdateClientsSelle
{
    public class UpdateClientsSelleValidatorErrorResult : 
        IUpdateClientsSelleErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateClientsSelleRequest:
        IPipelineNode<
            IUpdateClientsSelleRequestContract,
            IUpdateClientsSelleResultContract>
    {
        private readonly IPipelineNode<
            IUpdateClientsSelleRequestContract,
            IUpdateClientsSelleResultContract> _nextNode;

        public ValidatedUpdateClientsSelleRequest(
            IPipelineNode<
                IUpdateClientsSelleRequestContract,
                IUpdateClientsSelleResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateClientsSelleResultContract> Ask(
            IUpdateClientsSelleRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new UpdateClientsSelleValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}