using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateClientsSalle
{
    public class UpdateClientsSalleValidatorErrorResult : IUpdateClientsSalleErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateClientsSalleRequest:
        IPipelineNode<IUpdateClientsSalleRequestContract, IUpdateClientsSalleResultContract>
    {
        private readonly IPipelineNode<IUpdateClientsSalleRequestContract, IUpdateClientsSalleResultContract> _nextNode;

        public ValidatedUpdateClientsSalleRequest(
            IPipelineNode<IUpdateClientsSalleRequestContract, IUpdateClientsSalleResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateClientsSalleResultContract> Ask(
            IUpdateClientsSalleRequestContract input)
        {
            if (false)
            {
                return new UpdateClientsSalleValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}