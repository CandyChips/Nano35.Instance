using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.UpdateUnitsAddress
{
    public class ConvertedUpdateUnitsAddressOnHttpContext : 
        PipeInConvert
        <UpdateUnitsAddressHttpBody, 
            IActionResult,
            IUpdateUnitsAddressRequestContract, 
            IUpdateUnitsAddressResultContract>
    {
        public ConvertedUpdateUnitsAddressOnHttpContext(IPipeNode<IUpdateUnitsAddressRequestContract, IUpdateUnitsAddressResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(UpdateUnitsAddressHttpBody input)
        {
            var converted = new UpdateUnitsAddressRequestContract()
            {
                UnitId = input.UnitId,
                Address = input.Address
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IUpdateUnitsAddressSuccessResultContract success => new OkObjectResult(success),
                IUpdateUnitsAddressErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}