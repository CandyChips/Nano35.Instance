using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.UpdateUnitsName
{
    public class ConvertedUpdateUnitsNameOnHttpContext : 
        PipeInConvert
        <UpdateUnitsNameHttpBody, 
            IActionResult,
            IUpdateUnitsNameRequestContract, 
            IUpdateUnitsNameResultContract>
    {
        public ConvertedUpdateUnitsNameOnHttpContext(IPipeNode<IUpdateUnitsNameRequestContract, IUpdateUnitsNameResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(UpdateUnitsNameHttpBody input)
        {
            var converted = new UpdateUnitsNameRequestContract()
            {
                UnitId = input.UnitId,
                Name = input.Name
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IUpdateUnitsNameSuccessResultContract success => new OkObjectResult(success),
                IUpdateUnitsNameErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}