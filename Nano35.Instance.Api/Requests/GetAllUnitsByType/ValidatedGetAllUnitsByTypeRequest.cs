using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllUnitsByType
{
    public class GetAllUnitsByTypeValidatorErrorResult : IGetAllUnitsByTypeErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllUnitsByTypeRequest:
        IPipelineNode<IGetAllUnitsByTypeRequestContract, IGetAllUnitsByTypeResultContract>
    {
        private readonly IPipelineNode<IGetAllUnitsByTypeRequestContract, IGetAllUnitsByTypeResultContract> _nextNode;

        public ValidatedGetAllUnitsByTypeRequest(
            IPipelineNode<IGetAllUnitsByTypeRequestContract, IGetAllUnitsByTypeResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllUnitsByTypeResultContract> Ask(
            IGetAllUnitsByTypeRequestContract input)
        {
            if (false)
            {
                return new GetAllUnitsByTypeValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}