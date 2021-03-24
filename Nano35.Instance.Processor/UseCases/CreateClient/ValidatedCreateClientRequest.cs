using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.CreateClient
{
    public class CreateClientValidatorErrorResult : 
        ICreateClientErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedCreateClientRequest:
        PipeNodeBase<
            ICreateClientRequestContract, 
            ICreateClientResultContract>
    {

        public ValidatedCreateClientRequest(
            IPipeNode<ICreateClientRequestContract,
                ICreateClientResultContract> next) : base(next)
        {}

        public override async Task<ICreateClientResultContract> Ask(
            ICreateClientRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new CreateClientValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}