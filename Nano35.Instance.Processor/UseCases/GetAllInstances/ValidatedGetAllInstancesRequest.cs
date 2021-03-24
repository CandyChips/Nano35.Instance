using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetAllInstances
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
        public ValidatedGetAllInstancesRequest(
            IPipeNode<IGetAllInstancesRequestContract, IGetAllInstancesResultContract> next) : base(next)
        {}

        public override async Task<IGetAllInstancesResultContract> Ask(
            IGetAllInstancesRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllInstancesValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}