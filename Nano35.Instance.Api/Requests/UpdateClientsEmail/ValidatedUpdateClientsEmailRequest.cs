using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateClientsEmail
{
    public class UpdateClientsEmailValidatorErrorResult : IUpdateClientsEmailErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateClientsEmailRequest:
        IPipelineNode<IUpdateClientsEmailRequestContract, IUpdateClientsEmailResultContract>
    {
        private readonly IPipelineNode<IUpdateClientsEmailRequestContract, IUpdateClientsEmailResultContract> _nextNode;

        public ValidatedUpdateClientsEmailRequest(
            IPipelineNode<IUpdateClientsEmailRequestContract, IUpdateClientsEmailResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateClientsEmailResultContract> Ask(
            IUpdateClientsEmailRequestContract input)
        {
            if (false)
            {
                return new UpdateClientsEmailValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}