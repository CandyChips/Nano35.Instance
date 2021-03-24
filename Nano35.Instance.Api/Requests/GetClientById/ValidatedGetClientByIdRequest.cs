using System.Threading.Tasks;
using FluentValidation;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetClientById
{
    public class GetClientByIdValidatorErrorResult : IGetClientByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetClientByIdRequest:
        PipeNodeBase<IGetClientByIdRequestContract, IGetClientByIdResultContract>
    {
        private readonly IValidator<IGetClientByIdRequestContract> _validator;

        public ValidatedGetClientByIdRequest(
            IValidator<IGetClientByIdRequestContract> validator,
            IPipeNode<IGetClientByIdRequestContract, IGetClientByIdResultContract> next) :
            base(next)
        {
            _validator = validator;
        }

        public override async Task<IGetClientByIdResultContract> Ask(
            IGetClientByIdRequestContract input)
        {
            if (false)
            {
                return new GetClientByIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}