using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetUnitStringsByIds
{
    public class GetUnitStringsByIdsValidatorErrorResult :
        IGetUnitStringsByIdsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetUnitStringsByIdsRequest:
        PipeNodeBase<
            IGetUnitStringsByIdsRequestContract, 
            IGetUnitStringsByIdsResultContract>
    {

        public ValidatedGetUnitStringsByIdsRequest(
                IPipeNode<IGetUnitStringsByIdsRequestContract,
                IGetUnitStringsByIdsResultContract> next) : 
                base(next)
        {
        }

        public override async Task<IGetUnitStringsByIdsResultContract> Ask(
            IGetUnitStringsByIdsRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetUnitStringsByIdsValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}