using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.CreatePaymentOfSelle
{
    public class CreatePaymentOfSelleValidatorErrorResult : 
        ICreatePaymentOfSelleErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedCreatePaymentOfSelleRequest:
        IPipelineNode<
            ICreatePaymentOfSelleRequestContract,
            ICreatePaymentOfSelleResultContract>
    {
        private readonly IPipelineNode<
            ICreatePaymentOfSelleRequestContract,
            ICreatePaymentOfSelleResultContract> _nextNode;

        public ValidatedCreatePaymentOfSelleRequest(
            IPipelineNode<
                ICreatePaymentOfSelleRequestContract, 
                ICreatePaymentOfSelleResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<ICreatePaymentOfSelleResultContract> Ask(
            ICreatePaymentOfSelleRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new CreatePaymentOfSelleValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}