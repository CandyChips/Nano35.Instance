using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetInstanceById
{
    public class GetInstanceByIdValidatorErrorResult : 
        IGetInstanceByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetInstanceByIdRequest:
        PipeNodeBase<
            IGetInstanceByIdRequestContract, 
            IGetInstanceByIdResultContract>
    {
        public ValidatedGetInstanceByIdRequest(
            IPipeNode<IGetInstanceByIdRequestContract,
                IGetInstanceByIdResultContract> next) : base(next)
        { }

        public override async Task<IGetInstanceByIdResultContract> Ask(
            IGetInstanceByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetInstanceByIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}