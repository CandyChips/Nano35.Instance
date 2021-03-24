using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.GetAllUnits
{
    public class ConvertedGetAllUnitsOnHttpContext :
        PipeInConvert
        <GetAllUnitsHttpQuery, 
            IActionResult,
            IGetAllUnitsRequestContract, 
            IGetAllUnitsResultContract>
        {
        public ConvertedGetAllUnitsOnHttpContext(IPipeNode<IGetAllUnitsRequestContract, IGetAllUnitsResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(GetAllUnitsHttpQuery input)
        {
            var converted = new GetAllUnitsRequestContract();

            var response = await DoNext(converted);
            
            return response switch
            {
                IGetAllUnitsSuccessResultContract success => new OkObjectResult(success),
                IGetAllUnitsErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}

