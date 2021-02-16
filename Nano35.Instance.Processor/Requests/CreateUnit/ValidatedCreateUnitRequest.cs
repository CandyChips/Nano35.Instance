using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.CreateUnit
{
    public class CreateUnitValidatorErrorResult : ICreateUnitErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedCreateUnitRequest:
        IPipelineNode<ICreateUnitRequestContract, ICreateUnitResultContract>
    {
        private readonly IPipelineNode<ICreateUnitRequestContract, ICreateUnitResultContract> _nextNode;

        public ValidatedCreateUnitRequest(
            IPipelineNode<ICreateUnitRequestContract, ICreateUnitResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<ICreateUnitResultContract> Ask(ICreateUnitRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new CreateUnitValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}