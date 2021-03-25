using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.UpdateInstanceEmail
{
    public class ConvertedUpdateInstanceEmailOnHttpContext : 
        PipeInConvert
        <UpdateInstanceEmailHttpBody, 
            IActionResult,
            IUpdateInstanceEmailRequestContract, 
            IUpdateInstanceEmailResultContract>
    {
        public ConvertedUpdateInstanceEmailOnHttpContext(IPipeNode<IUpdateInstanceEmailRequestContract, IUpdateInstanceEmailResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(UpdateInstanceEmailHttpBody input)
        {
            var converted = new UpdateInstanceEmailRequestContract()
            {
                InstanceId = input.InstanceId,
                Email = input.Email
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IUpdateInstanceEmailSuccessResultContract success => new OkObjectResult(success),
                IUpdateInstanceEmailErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}