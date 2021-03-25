using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateUnitsName
{
    public class UpdateUnitsNameValidatorErrorResult : IUpdateUnitsNameErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateUnitsNameRequest:
        PipeNodeBase
        <IUpdateUnitsNameRequestContract,
            IUpdateUnitsNameResultContract>
    {
        public ValidatedUpdateUnitsNameRequest(
            IPipeNode<IUpdateUnitsNameRequestContract,
                IUpdateUnitsNameResultContract> next) : base(next)
        {
        }

        public override async Task<IUpdateUnitsNameResultContract> Ask(
            IUpdateUnitsNameRequestContract input)
        {
            if (false)
            {
                return new UpdateUnitsNameValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}