using System.Threading.Tasks;
using FluentValidation;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllUnitsByType
{
    public class GetAllUnitsByTypeValidatorErrorResult : IGetAllUnitsByTypeErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllUnitsByTypeRequest:
        PipeNodeBase<IGetAllUnitsByTypeRequestContract, IGetAllUnitsByTypeResultContract>
    {
        private readonly IValidator<IGetAllUnitsByTypeRequestContract> _validator;

        public ValidatedGetAllUnitsByTypeRequest(
            IValidator<IGetAllUnitsByTypeRequestContract> validator,
            IPipeNode<IGetAllUnitsByTypeRequestContract, IGetAllUnitsByTypeResultContract> next) :
            base(next)
        {
            _validator = validator;
        }

        public override async Task<IGetAllUnitsByTypeResultContract> Ask(
            IGetAllUnitsByTypeRequestContract input)
        {
            if (false)
            {
                return new GetAllUnitsByTypeValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}