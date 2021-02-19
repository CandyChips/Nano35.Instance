using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.GetClientById
{
    public class GetClientByIdValidatorErrorResult : IGetClientByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetClientByIdRequest:
        IPipelineNode<IGetClientByIdRequestContract, IGetClientByIdResultContract>
    {
        private readonly IPipelineNode<IGetClientByIdRequestContract, IGetClientByIdResultContract> _nextNode;

        public ValidatedGetClientByIdRequest(
            IPipelineNode<IGetClientByIdRequestContract, IGetClientByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetClientByIdResultContract> Ask(IGetClientByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetClientByIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}