using System.Threading.Tasks;
using FluentValidation;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetUnitById
{
    public class GetUnitByIdValidatorErrorResult : IGetUnitByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetUnitByIdRequest:
        PipeNodeBase<IGetUnitByIdRequestContract, IGetUnitByIdResultContract>
    {
        private readonly IValidator<IGetUnitByIdRequestContract> _validator;

        public ValidatedGetUnitByIdRequest(
            IValidator<IGetUnitByIdRequestContract> validator,
            IPipeNode<IGetUnitByIdRequestContract, IGetUnitByIdResultContract> next) :
            base(next)
        {
            _validator = validator;
        }

        public override async Task<IGetUnitByIdResultContract> Ask(
            IGetUnitByIdRequestContract input)
        {
            if (false)
            {
                return new GetUnitByIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}