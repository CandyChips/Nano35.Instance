using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.UpdateInstanceName
{
    public class ConvertedUpdateInstanceNameOnHttpContext : 
        PipeInConvert
        <UpdateInstanceNameHttpBody, 
            IActionResult,
            IUpdateInstanceNameRequestContract, 
            IUpdateInstanceNameResultContract>
    {
        public ConvertedUpdateInstanceNameOnHttpContext(IPipeNode<IUpdateInstanceNameRequestContract, IUpdateInstanceNameResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(UpdateInstanceNameHttpBody input)
        {
            var converted = new UpdateInstanceNameRequestContract()
            {
                InstanceId = input.InstanceId,
                Name = input.Name
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IUpdateInstanceNameSuccessResultContract success => new OkObjectResult(success),
                IUpdateInstanceNameErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}