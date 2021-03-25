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
        PipeNodeBase<
            IUpdateUnitsAddressRequestContract, 
            IUpdateUnitsAddressResultContract>
    {
        public ValidatedUpdateUnitsAddressRequest(
            IPipeNode<IUpdateUnitsAddressRequestContract,
                IUpdateUnitsAddressResultContract> next) : base(next)
        {}
        public override async Task<IUpdateUnitsAddressResultContract> Ask(
            IUpdateUnitsAddressRequestContract input)
        {
            if (false)
            {
                return new UpdateUnitsAddressValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}