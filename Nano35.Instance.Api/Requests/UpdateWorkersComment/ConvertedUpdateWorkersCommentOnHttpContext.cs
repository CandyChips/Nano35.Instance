using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.UpdateWorkersComment
{
    public class ConvertedUpdateWorkersCommentOnHttpContext : 
        PipeInConvert
        <UpdateWorkersCommentHttpBody, 
            IActionResult,
            IUpdateWorkersCommentRequestContract, 
            IUpdateWorkersCommentResultContract>
    {
        public ConvertedUpdateWorkersCommentOnHttpContext(IPipeNode<IUpdateWorkersCommentRequestContract, IUpdateWorkersCommentResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(UpdateWorkersCommentHttpBody input)
        {
            var converted = new UpdateWorkersCommentRequestContract()
            {
                WorkersId = input.WorkersId,
                Comment = input.Comment
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IUpdateWorkersCommentSuccessResultContract success => new OkObjectResult(success),
                IUpdateWorkersCommentErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}