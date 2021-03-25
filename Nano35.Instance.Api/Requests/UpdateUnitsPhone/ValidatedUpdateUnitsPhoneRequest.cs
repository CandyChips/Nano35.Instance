using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateUnitsPhone
{
    public class UpdateUnitsPhoneValidatorErrorResult : IUpdateUnitsPhoneErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateUnitsPhoneRequest:
        PipeNodeBase<IUpdateUnitsPhoneRequestContract, IUpdateUnitsPhoneResultContract>
    {

        public ValidatedUpdateUnitsPhoneRequest(
            IPipeNode<IUpdateUnitsPhoneRequestContract,
                IUpdateUnitsPhoneResultContract> next) : base(next)
        {}

        public override async Task<IUpdateUnitsPhoneResultContract> Ask(
            IUpdateUnitsPhoneRequestContract input)
        {
            if (false)
            {
                return new UpdateUnitsPhoneValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}