using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.GetAllUnitTypes
{
    public class ConvertedGetAllUnitTypesOnHttpContext :
        PipeInConvert
        <GetAllUnitTypesHttpQuery, 
            IActionResult,
            IGetAllUnitTypesRequestContract, 
            IGetAllUnitTypesResultContract>
        {
        public ConvertedGetAllUnitTypesOnHttpContext(IPipeNode<IGetAllUnitTypesRequestContract, IGetAllUnitTypesResultContract> next) : base(next) {}
        public override async Task<IActionResult> Ask(GetAllUnitTypesHttpQuery input) =>
            await DoNext(new GetAllUnitTypesRequestContract()) switch
                {
                    IGetAllUnitTypesSuccessResultContract success => new OkObjectResult(success),
                    IGetAllUnitTypesErrorResultContract error => new BadRequestObjectResult(error),
                    _ => new BadRequestObjectResult("")
                };
        }
}

