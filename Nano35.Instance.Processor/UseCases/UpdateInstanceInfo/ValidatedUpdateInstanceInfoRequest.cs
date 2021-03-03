using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.UpdateInstanceInfo
{
    public class UpdateInstanceInfoValidatorErrorResult : 
        IUpdateInstanceInfoErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateInstanceInfoRequest:
        IPipelineNode<
            IUpdateInstanceInfoRequestContract,
            IUpdateInstanceInfoResultContract>
    {
        private readonly IPipelineNode<
            IUpdateInstanceInfoRequestContract,
            IUpdateInstanceInfoResultContract> _nextNode;

        public ValidatedUpdateInstanceInfoRequest(
            IPipelineNode<
                IUpdateInstanceInfoRequestContract,
                IUpdateInstanceInfoResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateInstanceInfoResultContract> Ask(
            IUpdateInstanceInfoRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new UpdateInstanceInfoValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}