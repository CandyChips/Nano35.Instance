using System.Threading.Tasks;
using FluentValidation;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetWorkerById
{
    public class GetWorkerByIdValidatorErrorResult : IGetWorkerByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetWorkerByIdRequest:
        PipeNodeBase<IGetWorkerByIdRequestContract, IGetWorkerByIdResultContract>
    {
        private readonly IValidator<IGetWorkerByIdRequestContract> _validator;

        public ValidatedGetWorkerByIdRequest(
            IValidator<IGetWorkerByIdRequestContract> validator,
            IPipeNode<IGetWorkerByIdRequestContract, IGetWorkerByIdResultContract> next) :
            base(next)
        {
            _validator = validator;
        }

        public override async Task<IGetWorkerByIdResultContract> Ask(
            IGetWorkerByIdRequestContract input)
        {
            if (false)
            {
                return new GetWorkerByIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}