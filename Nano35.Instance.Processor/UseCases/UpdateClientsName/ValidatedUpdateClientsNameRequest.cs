using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsName
{
    public class UpdateClientsNameValidatorErrorResult : 
        IUpdateClientsNameErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateClientsNameRequest:
        PipeNodeBase<
            IUpdateClientsNameRequestContract,
            IUpdateClientsNameResultContract>
    {

        public ValidatedUpdateClientsNameRequest(
            IPipeNode<IUpdateClientsNameRequestContract,
                IUpdateClientsNameResultContract> next) : base(next)
        {}

        public override async Task<IUpdateClientsNameResultContract> Ask(
            IUpdateClientsNameRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new UpdateClientsNameValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}