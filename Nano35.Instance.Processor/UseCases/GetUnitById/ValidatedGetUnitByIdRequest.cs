using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetUnitById
{
    public class GetUnitByIdValidatorErrorResult :
        IGetUnitByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetUnitByIdRequest:
        PipeNodeBase<
            IGetUnitByIdRequestContract,
            IGetUnitByIdResultContract>
    {

        public ValidatedGetUnitByIdRequest(
            IPipeNode<IGetUnitByIdRequestContract,
                IGetUnitByIdResultContract> next) : base(next)
        {
        }

        public override async Task<IGetUnitByIdResultContract> Ask(
            IGetUnitByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetUnitByIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}