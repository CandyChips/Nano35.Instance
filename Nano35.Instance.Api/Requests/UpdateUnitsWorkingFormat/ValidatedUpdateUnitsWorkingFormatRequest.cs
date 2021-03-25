using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateUnitsWorkingFormat
{
    public class UpdateUnitsWorkingFormatValidatorErrorResult : IUpdateUnitsWorkingFormatErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateUnitsWorkingFormatRequest:
        PipeNodeBase
        <IUpdateUnitsWorkingFormatRequestContract,
            IUpdateUnitsWorkingFormatResultContract>
    {

        public ValidatedUpdateUnitsWorkingFormatRequest(
            IPipeNode<IUpdateUnitsWorkingFormatRequestContract,
                IUpdateUnitsWorkingFormatResultContract> next) : base(next)
        {}

        public override async Task<IUpdateUnitsWorkingFormatResultContract> Ask(
            IUpdateUnitsWorkingFormatRequestContract input)
        {
            if (false)
            {
                return new UpdateUnitsWorkingFormatValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}