using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetInstanceStringById
{
    public class GetInstanceStringByIdValidatorErrorResult :
        IGetInstanceStringByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetInstanceStringByIdRequest:
        PipeNodeBase<
            IGetInstanceStringByIdRequestContract, 
            IGetInstanceStringByIdResultContract>
    {

        public ValidatedGetInstanceStringByIdRequest(
            IPipeNode<IGetInstanceStringByIdRequestContract,
                IGetInstanceStringByIdResultContract> next) : base(next)
        {}

        public override async Task<IGetInstanceStringByIdResultContract> Ask(
            IGetInstanceStringByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetInstanceStringByIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}