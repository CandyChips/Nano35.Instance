using System.Threading.Tasks;
using FluentValidation;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Requests.GetAllInstances;

namespace Nano35.Instance.Api.Requests.GetAllCurrentInstances
{
    public class GetAllCurrentInstancesValidatorErrorResult : IGetAllInstancesErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllCurrentInstancesRequest:
        PipeNodeBase<IGetAllInstancesRequestContract, IGetAllInstancesResultContract>
    {
        private readonly IValidator<IGetAllInstancesRequestContract> _validator;

        public ValidatedGetAllCurrentInstancesRequest(
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