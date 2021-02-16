using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.CreateClient
{
    public class CreateClientValidatorErrorResult : ICreateClientErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedCreateClientRequest:
        IPipelineNode<ICreateClientRequestContract, ICreateClientResultContract>
    {
        private readonly IPipelineNode<ICreateClientRequestContract, ICreateClientResultContract> _nextNode;

        public ValidatedCreateClientRequest(
            IPipelineNode<ICreateClientRequestContract, ICreateClientResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<ICreateClientResultContract> Ask(ICreateClientRequestContract input, CancellationToken cancellationToken)
        {
            if (false)
            {
                return new CreateClientValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}