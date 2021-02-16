using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.GetAllClients
{
    public class GetAllClientsValidatorErrorResult : 
        IGetAllClientsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllClientsRequest:
        IPipelineNode<
            IGetAllClientsRequestContract, 
            IGetAllClientsResultContract>
    {
        private readonly IPipelineNode<
            IGetAllClientsRequestContract, 
            IGetAllClientsResultContract> _nextNode;

        public ValidatedGetAllClientsRequest(
            IPipelineNode<
                IGetAllClientsRequestContract, 
                IGetAllClientsResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllClientsResultContract> Ask(
            IGetAllClientsRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllClientsValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}