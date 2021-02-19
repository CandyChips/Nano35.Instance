using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.Requests.UpdateClientsPhone
{
    public class UpdateClientsPhoneValidatorErrorResult : 
        IUpdateClientsPhoneErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateClientsPhoneRequest:
        IPipelineNode<IUpdateClientsPhoneRequestContract, IUpdateClientsPhoneResultContract>
    {
        private readonly IPipelineNode<IUpdateClientsPhoneRequestContract, IUpdateClientsPhoneResultContract> _nextNode;

        public ValidatedUpdateClientsPhoneRequest(
            IPipelineNode<IUpdateClientsPhoneRequestContract, IUpdateClientsPhoneResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateClientsPhoneResultContract> Ask(
            IUpdateClientsPhoneRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new UpdateClientsPhoneValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input, cancellationToken);
        }
    }
}