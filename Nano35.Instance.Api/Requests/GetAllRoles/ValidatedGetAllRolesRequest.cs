using System.Threading.Tasks;
using FluentValidation;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllRoles
{
    public class GetAllRolesValidatorErrorResult : 
        IGetAllRolesErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllRolesRequest:
        PipeNodeBase<
            IGetAllRolesRequestContract, 
            IGetAllRolesResultContract>
    {
        private readonly IValidator<IGetAllRolesRequestContract> _validator;


        public ValidatedGetAllRolesRequest(
            IValidator<IGetAllRolesRequestContract> validator,
            IPipeNode<IGetAllRolesRequestContract, IGetAllRolesResultContract> next) :
            base(next)
        {
            _validator = validator;
        }

        public override async Task<IGetAllRolesResultContract> Ask(
            IGetAllRolesRequestContract input)
        {
            if (false)
            {
                return new GetAllRolesValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}