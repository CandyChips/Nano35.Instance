using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetAllClientsTypes
{
    public class GetAllClientTypesValidatorErrorResult : 
        IGetAllClientTypesErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllClientTypesRequest:
        PipeNodeBase<
            IGetAllClientTypesRequestContract, 
            IGetAllClientTypesResultContract>
    {

        public ValidatedGetAllClientTypesRequest(
            IPipeNode<IGetAllClientTypesRequestContract,
                IGetAllClientTypesResultContract> next) : base(next)
        {
        }

        public override async Task<IGetAllClientTypesResultContract> Ask(
            IGetAllClientTypesRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllClientTypesValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}