using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetInstanceStringById
{
    public class GetInstanceStringByIdValidatorErrorResult :
        IGetInstanceStringByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetInstanceStringByIdRequest:
        IPipelineNode<
            IGetInstanceStringByIdRequestContract, 
            IGetInstanceStringByIdResultContract>
    {
        private readonly IPipelineNode<
            IGetInstanceStringByIdRequestContract, 
            IGetInstanceStringByIdResultContract> _nextNode;

        public ValidatedGetInstanceStringByIdRequest(
            IPipelineNode<
                IGetInstanceStringByIdRequestContract, 
                IGetInstanceStringByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetInstanceStringByIdResultContract> Ask(
            IGetInstanceStringByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetInstanceStringByIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}