using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetWorkerStringsByIds
{
    public class GetWorkerStringsByIdsValidatorErrorResult :
        IGetWorkerStringsByIdsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetWorkerStringsByIdsRequest:
        PipeNodeBase<
            IGetWorkerStringsByIdsRequestContract, 
            IGetWorkerStringsByIdsResultContract>
    {
        public ValidatedGetWorkerStringsByIdsRequest(
            IPipeNode<IGetWorkerStringsByIdsRequestContract,
                IGetWorkerStringsByIdsResultContract> next) : base(next)
        {}

        public override async Task<IGetWorkerStringsByIdsResultContract> Ask(
            IGetWorkerStringsByIdsRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetWorkerStringsByIdsValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}