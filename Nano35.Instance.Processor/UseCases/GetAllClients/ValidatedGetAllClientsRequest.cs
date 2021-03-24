using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetAllClients
{
    public class GetAllClientsValidatorErrorResult : 
        IGetAllClientsErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllClientsRequest:
        PipeNodeBase<
            IGetAllClientsRequestContract, 
            IGetAllClientsResultContract>
    {
        public ValidatedGetAllClientsRequest(
            IPipeNode<IGetAllClientsRequestContract,
                IGetAllClientsResultContract> next) : base(next)
        {}

        public override async Task<IGetAllClientsResultContract> Ask(
            IGetAllClientsRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllClientsValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}