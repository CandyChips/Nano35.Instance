using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.UpdateInstanceRegion
{
    public class UpdateInstanceRegionValidatorErrorResult : 
        IUpdateInstanceRegionErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateInstanceRegionRequest:
        PipeNodeBase<
            IUpdateInstanceRegionRequestContract,
            IUpdateInstanceRegionResultContract>
    {
        public ValidatedUpdateInstanceRegionRequest(
            IPipeNode<IUpdateInstanceRegionRequestContract,
                IUpdateInstanceRegionResultContract> next) : base(next)
        {}

        public override async Task<IUpdateInstanceRegionResultContract> Ask(
            IUpdateInstanceRegionRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new UpdateInstanceRegionValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}