using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.UpdateInstanceRealName
{
    public class UpdateInstanceRealNameValidatorErrorResult : 
        IUpdateInstanceRealNameErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateInstanceRealNameRequest:
        PipeNodeBase<
            IUpdateInstanceRealNameRequestContract,
            IUpdateInstanceRealNameResultContract>
    {
        public ValidatedUpdateInstanceRealNameRequest(
            IPipeNode<IUpdateInstanceRealNameRequestContract,
                IUpdateInstanceRealNameResultContract> next) : base(next)
        {}

        public override async Task<IUpdateInstanceRealNameResultContract> Ask(
            IUpdateInstanceRealNameRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new UpdateInstanceRealNameValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}