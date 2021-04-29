using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.UpdateUnitsType
{
    public class ConvertedUpdateUnitsTypeOnHttpContext : 
        PipeInConvert
        <UpdateUnitsTypeHttpBody, 
            IActionResult,
            IUpdateUnitsTypeRequestContract, 
            IUpdateUnitsTypeResultContract>
    {
        public ConvertedUpdateUnitsTypeOnHttpContext(IPipeNode<IUpdateUnitsTypeRequestContract, IUpdateUnitsTypeResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(UpdateUnitsTypeHttpBody input)
        {
            var converted = new UpdateUnitsTypeRequestContract()
            {
                UnitId = input.UnitId,
                TypeId = input.TypeId
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IUpdateUnitsTypeSuccessResultContract success => new OkObjectResult(success),
                IUpdateUnitsTypeErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}