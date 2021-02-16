using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.CreateClient
{
    public class CreateClientValidationErrorResult : ICreateClientErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedCreateClientRequest:
        IPipelineNode<ICreateClientRequestContract, ICreateClientResultContract>
    {
        private readonly IPipelineNode<ICreateClientRequestContract, ICreateClientResultContract> _nextNode;

        public ValidatedCreateClientRequest(
            IPipelineNode<ICreateClientRequestContract, ICreateClientResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<ICreateClientResultContract> Ask(
            ICreateClientRequestContract input)
        {
            if (false)
            {
                return new CreateClientValidationErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}