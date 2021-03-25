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
        PipeNodeBase<
            IUpdateWorkersCommentRequestContract, 
            IUpdateWorkersCommentResultContract>
    {
        public ValidatedUpdateWorkersCommentRequest(
            IPipeNode<IUpdateWorkersCommentRequestContract,
                IUpdateWorkersCommentResultContract> next) : base(next)
        {}

        public override async Task<IUpdateWorkersCommentResultContract> Ask(
            IUpdateWorkersCommentRequestContract input)
        {
            if (false)
            {
                return new UpdateWorkersCommentValidatorErrorResult() {Message = "Ошибка валидации"};
            }
            return await DoNext(input);
        }
    }
}