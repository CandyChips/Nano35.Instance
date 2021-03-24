using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetAllUnitTypes
{
    public class GetAllUnitTypesValidatorErrorResult :
        IGetAllUnitTypesErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidateGetAllUnitTypesRequest:
        PipeNodeBase<
            IGetAllUnitTypesRequestContract, 
            IGetAllUnitTypesResultContract>
    {

        public ValidateGetAllUnitTypesRequest(
            IPipeNode<IGetAllUnitTypesRequestContract,
                IGetAllUnitTypesResultContract> next) : base(next)
        {}

        public override async Task<IGetAllUnitTypesResultContract> Ask(
            IGetAllUnitTypesRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllUnitTypesValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}