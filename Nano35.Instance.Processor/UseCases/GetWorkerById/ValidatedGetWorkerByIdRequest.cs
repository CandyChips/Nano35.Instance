using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetWorkerById
{
    public class GetWorkerByIdValidatorErrorResult : 
        IGetWorkerByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetWorkerByIdRequest:
        PipeNodeBase<
            IGetWorkerByIdRequestContract,
            IGetWorkerByIdResultContract>
    {
        public ValidatedGetWorkerByIdRequest(
            IPipeNode<IGetWorkerByIdRequestContract,
                IGetWorkerByIdResultContract> next) : base(next)
        {}
        
        public override async Task<IGetWorkerByIdResultContract> Ask(
            IGetWorkerByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetWorkerByIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}