using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.UpdateInstanceRegion
{
    public class ConvertedUpdateInstanceRegionOnHttpContext : 
        PipeInConvert
        <UpdateInstanceRegionHttpBody, 
            IActionResult,
            IUpdateInstanceRegionRequestContract, 
            IUpdateInstanceRegionResultContract>
    {
        public ConvertedUpdateInstanceRegionOnHttpContext(IPipeNode<IUpdateInstanceRegionRequestContract, IUpdateInstanceRegionResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(UpdateInstanceRegionHttpBody input)
        {
            var converted = new UpdateInstanceRegionRequestContract()
            {
                InstanceId = input.InstanceId,
                RegionId = input.RegionId
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IUpdateInstanceRegionSuccessResultContract success => new OkObjectResult(success),
                IUpdateInstanceRegionErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}