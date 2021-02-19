using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.CreateCashOutput
{
    public class CreateCashOutputValidatorErrorResult : ICreateCashOutputErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedCreateCashOutputRequest:
        IPipelineNode<ICreateCashOutputRequestContract, ICreateCashOutputResultContract>
    {
        private readonly IPipelineNode<ICreateCashOutputRequestContract, ICreateCashOutputResultContract> _nextNode;

        public ValidatedCreateCashOutputRequest(
            IPipelineNode<ICreateCashOutputRequestContract, ICreateCashOutputResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<ICreateCashOutputResultContract> Ask(ICreateCashOutputRequestContract input, CancellationToken cancellationToken)
        {
            if (false)
            {
                return new CreateCashOutputValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}