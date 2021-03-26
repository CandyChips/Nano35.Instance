using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.UpdateUnitsAddress
{
    public class UpdateUnitsAddressValidatorErrorResult : 
        IUpdateUnitsAddressErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateUnitsAddressRequest:
        PipeNodeBase<
            IUpdateUnitsAddressRequestContract,
            IUpdateUnitsAddressResultContract>
    {
        public ValidatedUpdateUnitsAddressRequest(
            IPipeNode<IUpdateUnitsAddressRequestContract,
                IUpdateUnitsAddressResultContract> next) : base(next)
        {
        }

        public override async Task<IUpdateUnitsAddressResultContract> Ask(
            IUpdateUnitsAddressRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new UpdateUnitsAddressValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}