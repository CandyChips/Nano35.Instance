using System.Threading.Tasks;
using FluentValidation;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetInstanceById
{
    public class GetInstanceByIdValidatorErrorResult : IGetInstanceByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetInstanceByIdRequest:
        PipeNodeBase<IGetInstanceByIdRequestContract, IGetInstanceByIdResultContract>
    {
        private readonly IValidator<IGetInstanceByIdRequestContract> _validator;

        public ValidatedGetInstanceByIdRequest(
            IValidator<IGetInstanceByIdRequestContract> validator,
            IPipeNode<IGetInstanceByIdRequestContract, IGetInstanceByIdResultContract> next) :
            base(next)
        {
            _validator = validator;
        }

        public override async Task<IGetInstanceByIdResultContract> Ask(
            IGetInstanceByIdRequestContract input)
        {
            if (false)
            {
                return new GetInstanceByIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}