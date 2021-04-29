using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.UpdateWorkersName
{
    public class ConvertedUpdateWorkersNameOnHttpContext : 
        PipeInConvert
        <UpdateWorkersNameHttpBody, 
            IActionResult,
            IUpdateWorkersNameRequestContract, 
            IUpdateWorkersNameResultContract>
    {
        public ConvertedUpdateWorkersNameOnHttpContext(IPipeNode<IUpdateWorkersNameRequestContract, IUpdateWorkersNameResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(UpdateWorkersNameHttpBody input)
        {
            var converted = new UpdateWorkersNameRequestContract()
            {
                WorkersId = input.WorkersId,
                Name = input.Name
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IUpdateWorkersNameSuccessResultContract success => new OkObjectResult(success),
                IUpdateWorkersNameErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}