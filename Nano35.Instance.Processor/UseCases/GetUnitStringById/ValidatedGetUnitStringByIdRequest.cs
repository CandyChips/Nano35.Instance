using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetUnitStringById
{
    public class GetUnitStringByIdValidatorErrorResult :
        IGetUnitStringByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetUnitStringByIdRequest:
        PipeNodeBase<
            IGetUnitStringByIdRequestContract, 
            IGetUnitStringByIdResultContract>
    {
        public ValidatedGetUnitStringByIdRequest(
            IPipeNode<IGetUnitStringByIdRequestContract,
                IGetUnitStringByIdResultContract> next) : base(next)
        {}

        public override async Task<IGetUnitStringByIdResultContract> Ask(
            IGetUnitStringByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetUnitStringByIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}