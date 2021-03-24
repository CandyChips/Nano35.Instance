using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetAllWorkerRoles
{
    public class GetAllWorkerRolesValidatorErrorResult : 
        IGetAllWorkerRolesErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllWorkerRolesRequest:
        PipeNodeBase<
            IGetAllWorkerRolesRequestContract,
            IGetAllWorkerRolesResultContract>
    {

        public ValidatedGetAllWorkerRolesRequest(
            IPipeNode<IGetAllWorkerRolesRequestContract,
                IGetAllWorkerRolesResultContract> next) : base(next)
        {}

        public override async Task<IGetAllWorkerRolesResultContract> Ask(
            IGetAllWorkerRolesRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllWorkerRolesValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}