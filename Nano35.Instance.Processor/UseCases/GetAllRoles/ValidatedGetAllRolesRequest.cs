using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetAllRoles
{
    public class GetAllRolesValidatorErrorResult :
        IGetAllRolesErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllRolesRequest:
        PipeNodeBase<
            IGetAllRolesRequestContract,
            IGetAllRolesResultContract>
    {
        public ValidatedGetAllRolesRequest(
            IPipeNode<IGetAllRolesRequestContract,
                IGetAllRolesResultContract> next) : base(next)
        {}

        public override async Task<IGetAllRolesResultContract> Ask(
            IGetAllRolesRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllRolesValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}