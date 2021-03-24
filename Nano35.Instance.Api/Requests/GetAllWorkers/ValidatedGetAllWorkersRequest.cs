using System.Threading.Tasks;
using FluentValidation;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllWorkers
{
    public class GetAllWorkersValidatorErrorResult : IGetAllWorkersErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllWorkersRequest:
        PipeNodeBase<IGetAllWorkersRequestContract, IGetAllWorkersResultContract>
    {
        private readonly IValidator<IGetAllWorkersRequestContract> _validator;

        public ValidatedGetAllWorkersRequest(
            IValidator<IGetAllWorkersRequestContract> validator,
            IPipeNode<IGetAllWorkersRequestContract, IGetAllWorkersResultContract> next) :
            base(next)
        {
            _validator = validator;
        }

        public override async Task<IGetAllWorkersResultContract> Ask(
            IGetAllWorkersRequestContract input)
        {
            if (false)
            {
                return new GetAllWorkersValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}