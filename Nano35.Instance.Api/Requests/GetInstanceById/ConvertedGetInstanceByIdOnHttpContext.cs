using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.GetInstanceById
{
    public class ConvertedGetInstanceByIdOnHttpContext :
        PipeInConvert
        <GetInstanceByIdHttpQuery, 
            IActionResult,
            IGetInstanceByIdRequestContract, 
            IGetInstanceByIdResultContract>
        {
        public ConvertedGetInstanceByIdOnHttpContext(IPipeNode<IGetInstanceByIdRequestContract, IGetInstanceByIdResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(GetInstanceByIdHttpQuery input)
        {
            var converted = new GetInstanceByIdRequestContract();

            var response = await DoNext(converted);
            
            return response switch
            {
                IGetInstanceByIdSuccessResultContract success => new OkObjectResult(success),
                IGetInstanceByIdErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}

