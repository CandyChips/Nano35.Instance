using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.UpdateClientsSelle
{
    public class UpdateClientsSelleValidatorErrorResult : 
        IUpdateClientsSelleErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateClientsSelleRequest:
        PipeNodeBase<
            IUpdateClientsSelleRequestContract,
            IUpdateClientsSelleResultContract>
    {

        public ValidatedUpdateClientsSelleRequest(
            IPipeNode<IUpdateClientsSelleRequestContract,
                IUpdateClientsSelleResultContract> next) : base(next)
        {
        }

        public override async Task<IUpdateClientsSelleResultContract> Ask(
            IUpdateClientsSelleRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new UpdateClientsSelleValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}