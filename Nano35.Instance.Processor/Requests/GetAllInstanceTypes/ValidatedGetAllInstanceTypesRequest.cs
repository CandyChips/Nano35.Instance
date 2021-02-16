using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.GetAllInstanceTypes
{
    public class GetAllInstanceTypesValidatorErrorResult : 
        IGetAllInstanceTypesErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllInstanceTypesRequest:
        IPipelineNode<
            IGetAllInstanceTypesRequestContract,
            IGetAllInstanceTypesResultContract>
    {
        private readonly IPipelineNode<
            IGetAllInstanceTypesRequestContract, 
            IGetAllInstanceTypesResultContract> _nextNode;

        public ValidatedGetAllInstanceTypesRequest(
            IPipelineNode<
                IGetAllInstanceTypesRequestContract,
                IGetAllInstanceTypesResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllInstanceTypesResultContract> Ask(
            IGetAllInstanceTypesRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllInstanceTypesValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}