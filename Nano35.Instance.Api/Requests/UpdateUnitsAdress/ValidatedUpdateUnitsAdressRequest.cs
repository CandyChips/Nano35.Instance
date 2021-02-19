using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateUnitsAdress
{
    public class UpdateUnitsAdressValidatorErrorResult : IUpdateUnitsAdressErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateUnitsAdressRequest:
        IPipelineNode<IUpdateUnitsAdressRequestContract, IUpdateUnitsAdressResultContract>
    {
        private readonly IPipelineNode<IUpdateUnitsAdressRequestContract, IUpdateUnitsAdressResultContract> _nextNode;

        public ValidatedUpdateUnitsAdressRequest(
            IPipelineNode<IUpdateUnitsAdressRequestContract, IUpdateUnitsAdressResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateUnitsAdressResultContract> Ask(
            IUpdateUnitsAdressRequestContract input)
        {
            if (false)
            {
                return new UpdateUnitsAdressValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}