using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.UpdateWorkersName
{
    public class UpdateWorkersNameValidatorErrorResult : 
        IUpdateWorkersNameErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateWorkersNameRequest:
        PipeNodeBase<
            IUpdateWorkersNameRequestContract,
            IUpdateWorkersNameResultContract>
    {
        public ValidatedUpdateWorkersNameRequest(
            IPipeNode<IUpdateWorkersNameRequestContract,
                IUpdateWorkersNameResultContract> next) : base(next)
        {}

        public override async Task<IUpdateWorkersNameResultContract> Ask(
            IUpdateWorkersNameRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new UpdateWorkersNameValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}