using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.GetAllClientsTypes
{
    public class GetAllClientTypesValidatorErrorResult : 
        IGetAllClientTypesErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class GetAllClientTypesValidator:
        IPipelineNode<
            IGetAllClientTypesRequestContract, 
            IGetAllClientTypesResultContract>
    {
        private readonly IPipelineNode<
            IGetAllClientTypesRequestContract,
            IGetAllClientTypesResultContract> _nextNode;

        public GetAllClientTypesValidator(
            IPipelineNode<
                IGetAllClientTypesRequestContract,
                IGetAllClientTypesResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllClientTypesResultContract> Ask(
            IGetAllClientTypesRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllClientTypesValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}