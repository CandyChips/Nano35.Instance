using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetClientStringById
{
    public class GetClientStringByIdValidatorErrorResult :
        IGetClientStringByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetClientStringByIdRequest:
        PipeNodeBase<
            IGetClientStringByIdRequestContract, 
            IGetClientStringByIdResultContract>
    {
        private readonly IPipelineNode<
            IGetClientStringByIdRequestContract, 
            IGetClientStringByIdResultContract> _nextNode;

        public ValidatedGetClientStringByIdRequest(
            IPipeNode<IGetClientStringByIdRequestContract,
                IGetClientStringByIdResultContract> next) : base(next)
        {}

        public override async Task<IGetClientStringByIdResultContract> Ask(
            IGetClientStringByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetClientStringByIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}