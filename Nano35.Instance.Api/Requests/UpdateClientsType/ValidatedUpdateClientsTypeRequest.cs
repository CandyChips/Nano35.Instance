using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateClientsType
{
    public class UpdateClientsTypeValidatorErrorResult : IUpdateClientsTypeErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateClientsTypeRequest:
        PipeNodeBase<IUpdateClientsTypeRequestContract, IUpdateClientsTypeResultContract>
    {

        public ValidatedUpdateClientsTypeRequest(
            IPipeNode<IUpdateClientsTypeRequestContract,
                IUpdateClientsTypeResultContract> next) : base(next)
        { }

        public override async Task<IUpdateClientsTypeResultContract> Ask(
            IUpdateClientsTypeRequestContract input)
        {
            if (false)
            {
                return new UpdateClientsTypeValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}