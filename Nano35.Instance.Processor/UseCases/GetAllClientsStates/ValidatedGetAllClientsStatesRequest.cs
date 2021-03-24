using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetAllClientsStates
{
    public class GetAllClientStatesValidatorErrorResult : 
        IGetAllClientStatesErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetAllClientStatesRequest:
        PipeNodeBase<
            IGetAllClientStatesRequestContract,
            IGetAllClientStatesResultContract>
    {
        

        public ValidatedGetAllClientStatesRequest(
            IPipeNode<IGetAllClientStatesRequestContract,
                IGetAllClientStatesResultContract> next) : base(next)
        {
        }

        public override async Task<IGetAllClientStatesResultContract> Ask(
            IGetAllClientStatesRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetAllClientStatesValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}