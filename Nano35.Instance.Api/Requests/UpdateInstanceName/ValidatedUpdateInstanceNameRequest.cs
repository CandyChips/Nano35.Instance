using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateInstanceName
{
    public class UpdateInstanceNameValidatorErrorResult : IUpdateInstanceNameErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateInstanceNameRequest:
        PipeNodeBase
        <IUpdateInstanceNameRequestContract,
            IUpdateInstanceNameResultContract>
    {

        public ValidatedUpdateInstanceNameRequest(
            IPipeNode<IUpdateInstanceNameRequestContract,
                IUpdateInstanceNameResultContract> next) : base(next)
        {}

        public override async Task<IUpdateInstanceNameResultContract> Ask(
            IUpdateInstanceNameRequestContract input)
        {
            if (false)
            {
                return new UpdateInstanceNameValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}