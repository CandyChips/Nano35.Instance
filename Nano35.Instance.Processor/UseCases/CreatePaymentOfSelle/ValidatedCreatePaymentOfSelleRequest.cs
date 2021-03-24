using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.CreatePaymentOfSelle
{
    public class CreatePaymentOfSelleValidatorErrorResult : 
        ICreatePaymentOfSelleErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedCreatePaymentOfSelleRequest:
        PipeNodeBase<
            ICreatePaymentOfSelleRequestContract,
            ICreatePaymentOfSelleResultContract>
    {

        public ValidatedCreatePaymentOfSelleRequest(
            IPipeNode<ICreatePaymentOfSelleRequestContract,
                ICreatePaymentOfSelleResultContract> next) : base(next)
        {}

        public override async Task<ICreatePaymentOfSelleResultContract> Ask(
            ICreatePaymentOfSelleRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new CreatePaymentOfSelleValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}