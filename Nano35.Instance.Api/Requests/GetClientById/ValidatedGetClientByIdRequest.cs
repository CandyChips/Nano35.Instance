using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetClientById
{
    public class GetClientByIdValidatorErrorResult : IGetClientByIdErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedGetClientByIdRequest:
        IPipelineNode<IGetClientByIdRequestContract, IGetClientByIdResultContract>
    {
        private readonly IPipelineNode<IGetClientByIdRequestContract, IGetClientByIdResultContract> _nextNode;

        public ValidatedGetClientByIdRequest(
            IPipelineNode<IGetClientByIdRequestContract, IGetClientByIdResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IGetClientByIdResultContract> Ask(
            IGetClientByIdRequestContract input)
        {
            if (false)
            {
                return new GetClientByIdValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}