using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetAllUnits
{
    public class GetAllUnitsValidatorErrorResult :
        IGetAllUnitsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllUnitsRequest:
        PipeNodeBase<
            IGetAllUnitsRequestContract,
            IGetAllUnitsResultContract>
    {

        public ValidatedGetAllUnitsRequest(
            IPipeNode<IGetAllUnitsRequestContract,
                IGetAllUnitsResultContract> next) : base(next)
        {}

        public override async Task<IGetAllUnitsResultContract> Ask(
            IGetAllUnitsRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllUnitsValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}