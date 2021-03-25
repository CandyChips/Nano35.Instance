using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.UpdateInstanceInfo
{
    public class ConvertedUpdateInstanceInfoOnHttpContext : 
        PipeInConvert
        <UpdateInstanceInfoHttpBody, 
            IActionResult,
            IUpdateInstanceInfoRequestContract, 
            IUpdateInstanceInfoResultContract>
    {
        public ConvertedUpdateInstanceInfoOnHttpContext(IPipeNode<IUpdateInstanceInfoRequestContract, IUpdateInstanceInfoResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(UpdateInstanceInfoHttpBody input)
        {
            var converted = new UpdateInstanceInfoRequestContract()
            {
                InstanceId = input.InstanceId,
                Info = input.Info
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IUpdateInstanceInfoSuccessResultContract success => new OkObjectResult(success),
                IUpdateInstanceInfoErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}