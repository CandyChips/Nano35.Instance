using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.CreateClient
{
    public class CreateClientValidatorErrorResult : ICreateClientErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class CreateClientValidator:
        IPipelineNode<ICreateClientRequestContract, ICreateClientResultContract>
    {
        private readonly IPipelineNode<ICreateClientRequestContract, ICreateClientResultContract> _nextNode;

        public CreateClientValidator(
            IPipelineNode<ICreateClientRequestContract, ICreateClientResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<ICreateClientResultContract> Ask(
            ICreateClientRequestContract input)
        {
            if (false)
            {
                return new CreateClientValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}