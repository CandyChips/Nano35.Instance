using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.GetAllClientStates
{
    public class ConvertedGetAllClientStatesOnHttpContext :
        PipeInConvert
        <GetAllClientStatesHttpQuery, 
            IActionResult,
            IGetAllClientStatesRequestContract, 
            IGetAllClientStatesResultContract>
        {
        public ConvertedGetAllClientStatesOnHttpContext(IPipeNode<IGetAllClientStatesRequestContract, IGetAllClientStatesResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(GetAllClientStatesHttpQuery input)
        {
            var converted = new GetAllClientStatesRequestContract();

            var response = await DoNext(converted);
            
            return response switch
            {
                IGetAllClientStatesSuccessResultContract success => new OkObjectResult(success),
                IGetAllClientStatesErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}

