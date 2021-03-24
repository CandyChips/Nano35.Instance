using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetAllCashOperations
{
    public class GetAllCashOperationsValidatorErrorResult :
        IGetAllCashOperationsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllCashOperationsRequest:
        PipeNodeBase<
            IGetAllCashOperationsRequestContract,
            IGetAllCashOperationsResultContract>
    {
        
        public ValidatedGetAllCashOperationsRequest(
            IPipeNode<IGetAllCashOperationsRequestContract,
                IGetAllCashOperationsResultContract> next) : base(next)
        {}

        public override async Task<IGetAllCashOperationsResultContract> Ask(
            IGetAllCashOperationsRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllCashOperationsValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}