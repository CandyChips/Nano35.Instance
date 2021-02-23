using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateWorkersRole
{
    public class UpdateWorkersRoleValidatorErrorResult :
        IUpdateWorkersRoleErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateWorkersRoleRequest:
        IPipelineNode<
            IUpdateWorkersRoleRequestContract, 
            IUpdateWorkersRoleResultContract>
    {
        private readonly IPipelineNode<
            IUpdateWorkersRoleRequestContract,
            IUpdateWorkersRoleResultContract> _nextNode;

        public ValidatedUpdateWorkersRoleRequest(
            IPipelineNode<
                IUpdateWorkersRoleRequestContract, 
                IUpdateWorkersRoleResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateWorkersRoleResultContract> Ask(
            IUpdateWorkersRoleRequestContract input)
        {
            if (false)
            {
                return new UpdateWorkersRoleValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}