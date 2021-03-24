using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.CreatePaymentOfComing
{
    public class CreatePaymentOfComingValidatorErrorResult : 
        ICreatePaymentOfComingErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedCreatePaymentOfComingRequest:
        PipeNodeBase<
            ICreatePaymentOfComingRequestContract,
            ICreatePaymentOfComingResultContract>
    {

        public ValidatedCreatePaymentOfComingRequest(
            IPipeNode<ICreatePaymentOfComingRequestContract, ICreatePaymentOfComingResultContract> next) : base(next)
        {}

        public override async Task<ICreatePaymentOfComingResultContract> Ask(
            ICreatePaymentOfComingRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new CreatePaymentOfComingValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}