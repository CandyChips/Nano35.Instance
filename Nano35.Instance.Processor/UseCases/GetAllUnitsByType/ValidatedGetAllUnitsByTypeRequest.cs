using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetAllUnitsByType
{
    public class GetAllUnitsByTypeValidatorErrorResult : 
        IGetAllUnitsByTypeErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllUnitsByTypeRequest:
        PipeNodeBase<
            IGetAllUnitsByTypeRequestContract,
            IGetAllUnitsByTypeResultContract>
    {
        public ValidatedGetAllUnitsByTypeRequest(
            IPipeNode<IGetAllUnitsByTypeRequestContract,
                IGetAllUnitsByTypeResultContract> next) : base(next)
        {}

        public override async Task<IGetAllUnitsByTypeResultContract> Ask(
            IGetAllUnitsByTypeRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllUnitsByTypeValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}