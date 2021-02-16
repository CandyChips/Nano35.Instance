using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.GetAllClientsStates
{
    public class GetAllClientStatesValidatorErrorResult : 
        IGetAllClientStatesErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllClientStatesRequest:
        IPipelineNode<
            IGetAllClientStatesRequestContract,
            IGetAllClientStatesResultContract>
    {
        private readonly IPipelineNode<
            IGetAllClientStatesRequestContract,
            IGetAllClientStatesResultContract> _nextNode;

        public ValidatedGetAllClientStatesRequest(
            IPipelineNode<
                IGetAllClientStatesRequestContract, 
                IGetAllClientStatesResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllClientStatesResultContract> Ask(
            IGetAllClientStatesRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllClientStatesValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}