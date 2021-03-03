using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetAllWorkerRoles
{
    public class GetAllWorkerRolesValidatorErrorResult : 
        IGetAllWorkerRolesErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllWorkerRolesRequest:
        IPipelineNode<
            IGetAllWorkerRolesRequestContract,
            IGetAllWorkerRolesResultContract>
    {
        private readonly IPipelineNode<
            IGetAllWorkerRolesRequestContract, 
            IGetAllWorkerRolesResultContract> _nextNode;

        public ValidatedGetAllWorkerRolesRequest(
            IPipelineNode<
                IGetAllWorkerRolesRequestContract, 
                IGetAllWorkerRolesResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllWorkerRolesResultContract> Ask(
            IGetAllWorkerRolesRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllWorkerRolesValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}