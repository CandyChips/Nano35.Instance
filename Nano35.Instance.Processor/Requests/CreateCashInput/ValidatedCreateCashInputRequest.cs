using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.CreateCashInput
{
    public class CreateCashInputValidatorErrorResult : 
        ICreateCashInputErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedCreateCashInputRequest:
        IPipelineNode<
            ICreateCashInputRequestContract,
            ICreateCashInputResultContract>
    {
        private readonly IPipelineNode<
            ICreateCashInputRequestContract, 
            ICreateCashInputResultContract> _nextNode;

        public ValidatedCreateCashInputRequest(
            IPipelineNode<
                ICreateCashInputRequestContract,
                ICreateCashInputResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<ICreateCashInputResultContract> Ask(
            ICreateCashInputRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new CreateCashInputValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}