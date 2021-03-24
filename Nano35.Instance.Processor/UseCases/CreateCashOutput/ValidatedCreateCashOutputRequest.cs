using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.CreateCashOutput
{
    public class CreateCashOutputValidatorErrorResult : 
        ICreateCashOutputErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedCreateCashOutputRequest:
        PipeNodeBase<
            ICreateCashOutputRequestContract, 
            ICreateCashOutputResultContract>
    {
        public ValidatedCreateCashOutputRequest(
            IPipeNode<ICreateCashOutputRequestContract,
                ICreateCashOutputResultContract> next) : base(next)
        {}

        public override async Task<ICreateCashOutputResultContract> Ask(
            ICreateCashOutputRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new CreateCashOutputValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}