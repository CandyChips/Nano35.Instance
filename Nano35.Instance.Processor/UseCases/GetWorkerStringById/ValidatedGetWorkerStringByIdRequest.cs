using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetWorkerStringById
{
    public class GetWorkerStringByIdValidatorErrorResult :
        IGetWorkerStringByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetWorkerStringByIdRequest:
        PipeNodeBase<
            IGetWorkerStringByIdRequestContract, 
            IGetWorkerStringByIdResultContract>
    {

        public ValidatedGetWorkerStringByIdRequest(
            IPipeNode<IGetWorkerStringByIdRequestContract, IGetWorkerStringByIdResultContract> next) : base(next)
        { }
        
        public override async Task<IGetWorkerStringByIdResultContract> Ask(
            IGetWorkerStringByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetWorkerStringByIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}