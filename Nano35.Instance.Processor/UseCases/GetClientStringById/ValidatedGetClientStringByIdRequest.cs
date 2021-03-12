using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetClientStringById
{
    public class GetClientStringByIdValidatorErrorResult :
        IGetClientStringByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetClientStringByIdRequest:
        IPipelineNode<
            IGetClientStringByIdRequestContract, 
            IGetClientStringByIdResultContract>
    {
        private readonly IPipelineNode<
            IGetClientStringByIdRequestContract, 
            IGetClientStringByIdResultContract> _nextNode;

        public ValidatedGetClientStringByIdRequest(
            IPipelineNode<
                IGetClientStringByIdRequestContract, 
                IGetClientStringByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetClientStringByIdResultContract> Ask(
            IGetClientStringByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetClientStringByIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}