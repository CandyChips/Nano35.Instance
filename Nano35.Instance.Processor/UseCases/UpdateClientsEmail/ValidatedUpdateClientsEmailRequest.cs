using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsEmail
{
    public class UpdateClientsEmailValidatorErrorResult : 
        IUpdateClientsEmailErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateClientsEmailRequest:
        PipeNodeBase<
            IUpdateClientsEmailRequestContract, 
            IUpdateClientsEmailResultContract>
    {
        public ValidatedUpdateClientsEmailRequest(
            IPipeNode<IUpdateClientsEmailRequestContract,
                IUpdateClientsEmailResultContract> next) : base(next)
        {}

        public override async Task<IUpdateClientsEmailResultContract> Ask(
            IUpdateClientsEmailRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new UpdateClientsEmailValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}