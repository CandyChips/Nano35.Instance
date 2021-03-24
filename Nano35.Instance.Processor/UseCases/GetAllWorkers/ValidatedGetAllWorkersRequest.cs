using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetAllWorkers
{
    public class GetAllWorkersValidatorErrorResult : 
        IGetAllWorkersErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllWorkersRequest:
        PipeNodeBase<
            IGetAllWorkersRequestContract, 
            IGetAllWorkersResultContract>
    {
        public ValidatedGetAllWorkersRequest(
            IPipeNode<IGetAllWorkersRequestContract,
                IGetAllWorkersResultContract> next) : base(next)
        {}

        public override async Task<IGetAllWorkersResultContract> Ask(
            IGetAllWorkersRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllWorkersValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}