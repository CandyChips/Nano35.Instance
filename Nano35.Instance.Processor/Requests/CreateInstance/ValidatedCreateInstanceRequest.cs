using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.CreateInstance
{
    public class CreateInstanceValidatorErrorResult : ICreateInstanceErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedCreateInstanceRequest:
        IPipelineNode<ICreateInstanceRequestContract, ICreateInstanceResultContract>
    {
        private readonly IPipelineNode<ICreateInstanceRequestContract, ICreateInstanceResultContract> _nextNode;

        public ValidatedCreateInstanceRequest(
            IPipelineNode<ICreateInstanceRequestContract, ICreateInstanceResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<ICreateInstanceResultContract> Ask(ICreateInstanceRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new CreateInstanceValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}