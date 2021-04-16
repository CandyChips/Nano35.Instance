using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.GetUnitById
{
    public class ConvertedGetUnitByIdOnHttpContext :
        PipeInConvert
        <GetUnitByIdHttpQuery, 
            IActionResult,
            IGetUnitByIdRequestContract, 
            IGetUnitByIdResultContract>
        {
        public ConvertedGetUnitByIdOnHttpContext(IPipeNode<IGetUnitByIdRequestContract, IGetUnitByIdResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(GetUnitByIdHttpQuery input)
        {
            var converted = new GetUnitByIdRequestContract()
            {
                UnitId = input.Id
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IGetUnitByIdSuccessResultContract success => new OkObjectResult(success),
                IGetUnitByIdErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}

