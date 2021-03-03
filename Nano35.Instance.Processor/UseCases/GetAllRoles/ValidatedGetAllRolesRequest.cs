using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetAllRoles
{
    public class GetAllRolesValidatorErrorResult :
        IGetAllRolesErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllRolesRequest:
        IPipelineNode<
            IGetAllRolesRequestContract,
            IGetAllRolesResultContract>
    {
        private readonly IPipelineNode<
            IGetAllRolesRequestContract,
            IGetAllRolesResultContract> _nextNode;

        public ValidatedGetAllRolesRequest(
            IPipelineNode<
                IGetAllRolesRequestContract,
                IGetAllRolesResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllRolesResultContract> Ask(
            IGetAllRolesRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllRolesValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}