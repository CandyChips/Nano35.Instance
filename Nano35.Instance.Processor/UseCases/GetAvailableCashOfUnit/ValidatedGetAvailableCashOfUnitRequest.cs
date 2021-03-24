using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetAvailableCashOfUnit
{
    public class GetAvailableCashOfUnitValidatorErrorResult :
        IGetAvailableCashOfUnitErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAvailableCashOfUnitRequest:
        PipeNodeBase<
            IGetAvailableCashOfUnitRequestContract, 
            IGetAvailableCashOfUnitResultContract>
    {

        public ValidatedGetAvailableCashOfUnitRequest(
            IPipeNode<IGetAvailableCashOfUnitRequestContract,
                IGetAvailableCashOfUnitResultContract> next) : base(next)
        {}
        
        public override async Task<IGetAvailableCashOfUnitResultContract> Ask(
            IGetAvailableCashOfUnitRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAvailableCashOfUnitValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}