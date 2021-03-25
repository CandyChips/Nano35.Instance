using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateUnitsType
{
    public class UpdateUnitsTypeValidatorErrorResult : IUpdateUnitsTypeErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateUnitsTypeRequest:
        PipeNodeBase
        <IUpdateUnitsTypeRequestContract,
            IUpdateUnitsTypeResultContract>
    {

        public ValidatedUpdateUnitsTypeRequest(
            IPipeNode<IUpdateUnitsTypeRequestContract,
                IUpdateUnitsTypeResultContract> next) : base(next)
        {}

        public override async Task<IUpdateUnitsTypeResultContract> Ask(
            IUpdateUnitsTypeRequestContract input)
        {
            if (false)
            {
                return new UpdateUnitsTypeValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}