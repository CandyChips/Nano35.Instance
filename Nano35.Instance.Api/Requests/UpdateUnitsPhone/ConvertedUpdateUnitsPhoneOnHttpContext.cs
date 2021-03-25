using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.UpdateUnitsPhone
{
    public class ConvertedUpdateUnitsPhoneOnHttpContext : 
        PipeInConvert
        <UpdateUnitsPhoneHttpBody, 
            IActionResult,
            IUpdateUnitsPhoneRequestContract, 
            IUpdateUnitsPhoneResultContract>
    {
        public ConvertedUpdateUnitsPhoneOnHttpContext(IPipeNode<IUpdateUnitsPhoneRequestContract, IUpdateUnitsPhoneResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(UpdateUnitsPhoneHttpBody input)
        {
            var converted = new UpdateUnitsPhoneRequestContract()
            {
                UnitId = input.UnitId,
                Phone = input.Phone
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IUpdateUnitsPhoneSuccessResultContract success => new OkObjectResult(success),
                IUpdateUnitsPhoneErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}