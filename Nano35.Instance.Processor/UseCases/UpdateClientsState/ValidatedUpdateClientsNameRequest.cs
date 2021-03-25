using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsState
{
    public class UpdateClientsStateValidatorErrorResult : 
        IUpdateClientsStateErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateClientsStateRequest:
        PipeNodeBase<
            IUpdateClientsStateRequestContract,
            IUpdateClientsStateResultContract>
    {

        public ValidatedUpdateClientsStateRequest(
            IPipeNode<IUpdateClientsStateRequestContract,
                IUpdateClientsStateResultContract> next) : base(next)
        {}

        public override async Task<IUpdateClientsStateResultContract> Ask(
            IUpdateClientsStateRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new UpdateClientsStateValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}