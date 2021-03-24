using System.Threading.Tasks;
using FluentValidation;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAvailableCashOfUnit
{
    public class GetAvailableCashOfUnitValidatorErrorResult : IGetAvailableCashOfUnitErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAvailableCashOfUnitRequest:
        PipeNodeBase<IGetAvailableCashOfUnitRequestContract, IGetAvailableCashOfUnitResultContract>
    {
        private readonly IValidator<IGetAvailableCashOfUnitRequestContract> _validator;

        public ValidatedGetAvailableCashOfUnitRequest(
            IValidator<IGetAvailableCashOfUnitRequestContract> validator,
            IPipeNode<IGetAvailableCashOfUnitRequestContract, IGetAvailableCashOfUnitResultContract> next) :
            base(next)
        {
            _validator = validator;
        }

        public override async Task<IGetAvailableCashOfUnitResultContract> Ask(
            IGetAvailableCashOfUnitRequestContract input)
        {
            if (false)
            {
                return new GetAvailableCashOfUnitValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}