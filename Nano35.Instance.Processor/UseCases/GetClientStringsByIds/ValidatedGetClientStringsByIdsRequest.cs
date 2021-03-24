using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetClientStringsByIds
{
    public class GetClientStringsByIdsValidatorErrorResult :
        IGetClientStringsByIdsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetClientStringsByIdRequest:
        PipeNodeBase<
            IGetClientStringsByIdsRequestContract, 
            IGetClientStringsByIdsResultContract>
    {
        public ValidatedGetClientStringsByIdRequest(
                IPipeNode<IGetClientStringsByIdsRequestContract, 
                IGetClientStringsByIdsResultContract> next) :
            base(next)
        {}

        public override async Task<IGetClientStringsByIdsResultContract> Ask(
            IGetClientStringsByIdsRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetClientStringsByIdsValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}