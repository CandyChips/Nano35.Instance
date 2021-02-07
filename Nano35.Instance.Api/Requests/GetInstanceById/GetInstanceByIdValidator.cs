using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetInstanceById
{
    public class GetInstanceByIdValidatorErrorResult : IGetInstanceByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class GetInstanceByIdValidator:
        IPipelineNode<IGetInstanceByIdRequestContract, IGetInstanceByIdResultContract>
    {
        private readonly IPipelineNode<IGetInstanceByIdRequestContract, IGetInstanceByIdResultContract> _nextNode;

        public GetInstanceByIdValidator(
            IPipelineNode<IGetInstanceByIdRequestContract, IGetInstanceByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetInstanceByIdResultContract> Ask(
            IGetInstanceByIdRequestContract input)
        {
            if (false)
            {
                return new GetInstanceByIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}