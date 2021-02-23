using System.Threading.Tasks;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.UpdateWorkersComment
{
    public class UpdateWorkersCommentValidatorErrorResult :
        IUpdateWorkersCommentErrorResultContract
    {
        public string Message { get; set; }
    }
    
    public class ValidatedUpdateWorkersCommentRequest:
        IPipelineNode<
            IUpdateWorkersCommentRequestContract, 
            IUpdateWorkersCommentResultContract>
    {
        private readonly IPipelineNode<
            IUpdateWorkersCommentRequestContract,
            IUpdateWorkersCommentResultContract> _nextNode;

        public ValidatedUpdateWorkersCommentRequest(
            IPipelineNode<
                IUpdateWorkersCommentRequestContract, 
                IUpdateWorkersCommentResultContract> nextNode)
        {
            _nextNode = nextNode;
        }

        public async Task<IUpdateWorkersCommentResultContract> Ask(
            IUpdateWorkersCommentRequestContract input)
        {
            if (false)
            {
                return new UpdateWorkersCommentValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await _nextNode.Ask(input);
        }
    }
}