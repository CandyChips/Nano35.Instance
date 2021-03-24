using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetAllInstanceTypes
{
    public class GetAllInstanceTypesValidatorErrorResult : 
        IGetAllInstanceTypesErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllInstanceTypesRequest:
        PipeNodeBase<
            IGetAllInstanceTypesRequestContract,
            IGetAllInstanceTypesResultContract>
    {
        public ValidatedGetAllInstanceTypesRequest(
            IPipeNode<IGetAllInstanceTypesRequestContract,
                IGetAllInstanceTypesResultContract> next) : base(next)
        {
        }

        public override async Task<IGetAllInstanceTypesResultContract> Ask(
            IGetAllInstanceTypesRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllInstanceTypesValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}