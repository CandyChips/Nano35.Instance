using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsPhone
{
    public class UpdateClientsPhoneValidatorErrorResult : 
        IUpdateClientsPhoneErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateClientsPhoneRequest:
        PipeNodeBase<
            IUpdateClientsPhoneRequestContract,
            IUpdateClientsPhoneResultContract>
    {
        public ValidatedUpdateClientsPhoneRequest(
            IPipeNode<IUpdateClientsPhoneRequestContract,
                IUpdateClientsPhoneResultContract> next) : base(next)
        {}

        public override async Task<IUpdateClientsPhoneResultContract> Ask(
            IUpdateClientsPhoneRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new UpdateClientsPhoneValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}