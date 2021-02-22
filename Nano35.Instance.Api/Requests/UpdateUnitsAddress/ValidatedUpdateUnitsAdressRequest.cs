using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateUnitsAddress
{
    public class UpdateUnitsAddressValidatorErrorResult :
        IUpdateUnitsAddressErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateUnitsAddressRequest:
        IPipelineNode<
            IUpdateUnitsAddressRequestContract, 
            IUpdateUnitsAddressResultContract>
    {
        private readonly IPipelineNode<
            IUpdateUnitsAddressRequestContract, 
            IUpdateUnitsAddressResultContract> _nextNode;

        public ValidatedUpdateUnitsAddressRequest(
            IPipelineNode<
                IUpdateUnitsAddressRequestContract,
                IUpdateUnitsAddressResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateUnitsAddressResultContract> Ask(
            IUpdateUnitsAddressRequestContract input)
        {
            if (false)
            {
                return new UpdateUnitsAddressValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}