using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.GetAllRegions
{
    public class ConvertedGetAllRegionsOnHttpContext :
        PipeInConvert
        <GetAllRegionsHttpQuery, 
            IActionResult,
            IGetAllRegionsRequestContract, 
            IGetAllRegionsResultContract>
        {
        public ConvertedGetAllRegionsOnHttpContext(IPipeNode<IGetAllRegionsRequestContract, IGetAllRegionsResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(GetAllRegionsHttpQuery input)
        {
            var converted = new GetAllRegionsRequestContract();

            var response = await DoNext(converted);
            
            return response switch
            {
                IGetAllRegionsSuccessResultContract success => new OkObjectResult(success),
                IGetAllRegionsErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}

