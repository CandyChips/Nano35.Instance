using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.CreateInstance
{
    public class CreateInstanceValidatorErrorResult : ICreateInstanceErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class CreateInstanceValidator:
        IPipelineNode<ICreateInstanceRequestContract, ICreateInstanceResultContract>
    {
        private readonly IPipelineNode<ICreateInstanceRequestContract, ICreateInstanceResultContract> _nextNode;

        public CreateInstanceValidator(
            IPipelineNode<ICreateInstanceRequestContract, ICreateInstanceResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<ICreateInstanceResultContract> Ask(
            ICreateInstanceRequestContract input)
        {
            if (false)
            {
                return new CreateInstanceValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}