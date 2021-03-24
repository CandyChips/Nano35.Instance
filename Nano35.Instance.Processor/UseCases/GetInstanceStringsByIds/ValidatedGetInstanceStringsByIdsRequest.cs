using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetInstanceStringsByIds
{
    public class GetInstanceStringsByIdsValidatorErrorResult :
        IGetInstanceStringsByIdsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetInstanceStringsByIdsRequest:
        PipeNodeBase<
            IGetInstanceStringsByIdsRequestContract, 
            IGetInstanceStringsByIdsResultContract>
    {
        public ValidatedGetInstanceStringsByIdsRequest(
                IPipeNode<IGetInstanceStringsByIdsRequestContract,
                IGetInstanceStringsByIdsResultContract> next) : 
            base(next)
        {}

        public override async Task<IGetInstanceStringsByIdsResultContract> Ask(
            IGetInstanceStringsByIdsRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetInstanceStringsByIdsValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}