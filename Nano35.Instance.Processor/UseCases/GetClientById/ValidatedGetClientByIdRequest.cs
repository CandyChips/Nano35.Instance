using System.Threading;
using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Processor.UseCases.GetClientById
{
    public class GetClientByIdValidatorErrorResult : 
        IGetClientByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetClientByIdRequest:
        PipeNodeBase<
            IGetClientByIdRequestContract, 
            IGetClientByIdResultContract>
    {

        public ValidatedGetClientByIdRequest(
            IPipeNode<IGetClientByIdRequestContract,
                IGetClientByIdResultContract> next) : base(next)
        {}

        public override async Task<IGetClientByIdResultContract> Ask(
            IGetClientByIdRequestContract input,
            CancellationToken cancellationToken)
        {
            if (false)
            {
                return new GetClientByIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input, cancellationToken);
        }
    }
}