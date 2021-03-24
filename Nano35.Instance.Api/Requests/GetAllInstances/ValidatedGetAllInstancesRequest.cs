using System.Threading.Tasks;
using FluentValidation;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetAllInstances
{
    public class GetAllInstancesValidatorErrorResult :
        IGetAllInstancesErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllInstancesRequest:
        PipeNodeBase<
            IGetAllInstancesRequestContract,
            IGetAllInstancesResultContract>
    {
        private readonly IValidator<IGetAllInstancesRequestContract> _validator;

        public ValidatedGetAllInstancesRequest(
            IValidator<IGetAllInstancesRequestContract> validator,
            IPipeNode<IGetAllInstancesRequestContract, IGetAllInstancesResultContract> next) :
            base(next)
        {
            _validator = validator;
        }

        public override async Task<IGetAllInstancesResultContract> Ask(
            IGetAllInstancesRequestContract input)
        {
            if (false)
            {
                return new GetAllInstancesValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}