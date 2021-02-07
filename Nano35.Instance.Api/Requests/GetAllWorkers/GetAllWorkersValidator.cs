using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllWorkers
{
    public class GetAllWorkersValidatorErrorResult : IGetAllWorkersErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class GetAllWorkersValidator:
        IPipelineNode<IGetAllWorkersRequestContract, IGetAllWorkersResultContract>
    {
        private readonly IPipelineNode<IGetAllWorkersRequestContract, IGetAllWorkersResultContract> _nextNode;

        public GetAllWorkersValidator(
            IPipelineNode<IGetAllWorkersRequestContract, IGetAllWorkersResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetAllWorkersResultContract> Ask(
            IGetAllWorkersRequestContract input)
        {
            if (false)
            {
                return new GetAllWorkersValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}