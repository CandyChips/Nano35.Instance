using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.CreateUnit
{
    public class CreateUnitValidatorErrorResult : 
        ICreateUnitErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedCreateUnitRequest:
        PipeNodeBase<
            ICreateUnitRequestContract, 
            ICreateUnitResultContract>
    {
        

        public ValidatedCreateUnitRequest(
            IPipeNode<ICreateUnitRequestContract,
                ICreateUnitResultContract> next) : base(next)
        {}

        public override async Task<ICreateUnitResultContract> Ask(
            ICreateUnitRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new CreateUnitValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}