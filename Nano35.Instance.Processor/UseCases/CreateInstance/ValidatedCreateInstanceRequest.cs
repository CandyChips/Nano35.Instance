using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.CreateInstance
{
    public class CreateInstanceValidatorErrorResult :
        ICreateInstanceErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedCreateInstanceRequest:
        PipeNodeBase<
            ICreateInstanceRequestContract,
            ICreateInstanceResultContract>
    {
       

        public ValidatedCreateInstanceRequest(
            IPipeNode<ICreateInstanceRequestContract,
                ICreateInstanceResultContract> next) : base(next)
        {}

        public override async Task<ICreateInstanceResultContract> Ask(
            ICreateInstanceRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new CreateInstanceValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}