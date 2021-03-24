using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetAllRegions
{
    public class GetAllRegionsValidatorErrorResult :
        IGetAllRegionsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllRegionsRequest:
        PipeNodeBase<
            IGetAllRegionsRequestContract, 
            IGetAllRegionsResultContract>
    {

        public ValidatedGetAllRegionsRequest(
            IPipeNode<IGetAllRegionsRequestContract,
                IGetAllRegionsResultContract> next) : base(next)
        {}

        public override async Task<IGetAllRegionsResultContract> Ask(
            IGetAllRegionsRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllRegionsValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}