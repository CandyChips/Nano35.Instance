using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.CreateCashInput
{
    public class CreateCashInputValidatorErrorResult : 
        ICreateCashInputErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedCreateCashInputRequest:
        PipeNodeBase<
            ICreateCashInputRequestContract,
            ICreateCashInputResultContract>
    {
        public ValidatedCreateCashInputRequest(
            
            IPipeNode<ICreateCashInputRequestContract, ICreateCashInputResultContract> next) : base(next)
        {}

        public override async Task<ICreateCashInputResultContract> Ask(
            ICreateCashInputRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new CreateCashInputValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}