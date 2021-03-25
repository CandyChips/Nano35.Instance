using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.UpdateInstanceRealName
{
    public class ConvertedUpdateInstanceRealNameOnHttpContext : 
        PipeInConvert
        <UpdateInstanceRealNameHttpBody, 
            IActionResult,
            IUpdateInstanceRealNameRequestContract, 
            IUpdateInstanceRealNameResultContract>
    {
        public ConvertedUpdateInstanceRealNameOnHttpContext(IPipeNode<IUpdateInstanceRealNameRequestContract, IUpdateInstanceRealNameResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(UpdateInstanceRealNameHttpBody input)
        {
            var converted = new UpdateInstanceRealNameRequestContract()
            {
                InstanceId = input.InstanceId,
                RealName = input.RealName
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IUpdateInstanceRealNameSuccessResultContract success => new OkObjectResult(success),
                IUpdateInstanceRealNameErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}