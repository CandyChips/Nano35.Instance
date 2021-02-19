using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.UpdateInstanceEmail
{
    public class UpdateInstanceEmailValidatorErrorResult : 
        IUpdateInstanceEmailErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateInstanceEmailRequest:
        IPipelineNode<IUpdateInstanceEmailRequestContract, IUpdateInstanceEmailResultContract>
    {
        private readonly IPipelineNode<IUpdateInstanceEmailRequestContract, IUpdateInstanceEmailResultContract> _nextNode;

        public ValidatedUpdateInstanceEmailRequest(
            IPipelineNode<IUpdateInstanceEmailRequestContract, IUpdateInstanceEmailResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateInstanceEmailResultContract> Ask(
            IUpdateInstanceEmailRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new UpdateInstanceEmailValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}