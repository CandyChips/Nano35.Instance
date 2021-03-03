using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.UpdateInstanceRealName
{
    public class UpdateInstanceRealNameValidatorErrorResult : 
        IUpdateInstanceRealNameErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateInstanceRealNameRequest:
        IPipelineNode<
            IUpdateInstanceRealNameRequestContract,
            IUpdateInstanceRealNameResultContract>
    {
        private readonly IPipelineNode<
            IUpdateInstanceRealNameRequestContract,
            IUpdateInstanceRealNameResultContract> _nextNode;

        public ValidatedUpdateInstanceRealNameRequest(
            IPipelineNode<
                IUpdateInstanceRealNameRequestContract,
                IUpdateInstanceRealNameResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateInstanceRealNameResultContract> Ask(
            IUpdateInstanceRealNameRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new UpdateInstanceRealNameValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}