using System.Threading.Tasks;
using FluentValidation;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllUnits
{
    public class GetAllUnitsValidatorErrorResult :
        IGetAllUnitsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllUnitsRequest:
        PipeNodeBase<
            IGetAllUnitsRequestContract, 
            IGetAllUnitsResultContract>
    {
        private readonly IValidator<IGetAllUnitsRequestContract> _validator;


        public ValidatedGetAllUnitsRequest(
            IValidator<IGetAllUnitsRequestContract> validator,
            IPipeNode<IGetAllUnitsRequestContract, IGetAllUnitsResultContract> next) :
            base(next)
        {
            _validator = validator;
        }

        public override async Task<IGetAllUnitsResultContract> Ask(
            IGetAllUnitsRequestContract input)
        {
            if (false)
            {
                return new GetAllUnitsValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}