using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateInstanceEmail
{
    public class UpdateInstanceEmailValidatorErrorResult : IUpdateInstanceEmailErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateInstanceEmailRequest:
        PipeNodeBase
        <IUpdateInstanceEmailRequestContract,
            IUpdateInstanceEmailResultContract>
    {

        public ValidatedUpdateInstanceEmailRequest(
            IPipeNode<IUpdateInstanceEmailRequestContract,
                IUpdateInstanceEmailResultContract> next) : base(next)
        {}

        public override async Task<IUpdateInstanceEmailResultContract> Ask(
            IUpdateInstanceEmailRequestContract input)
        {
            if (false)
            {
                return new UpdateInstanceEmailValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}