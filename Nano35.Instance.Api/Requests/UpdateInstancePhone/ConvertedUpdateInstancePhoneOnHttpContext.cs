using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.UpdateInstancePhone
{
    public class ConvertedUpdateInstancePhoneOnHttpContext : 
        PipeInConvert
        <UpdateInstancePhoneHttpBody, 
            IActionResult,
            IUpdateInstancePhoneRequestContract, 
            IUpdateInstancePhoneResultContract>
    {
        public ConvertedUpdateInstancePhoneOnHttpContext(IPipeNode<IUpdateInstancePhoneRequestContract, IUpdateInstancePhoneResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(UpdateInstancePhoneHttpBody input)
        {
            var converted = new UpdateInstancePhoneRequestContract()
            {
             InstanceId = input.InstanceId,
             Phone = input.Phone
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IUpdateInstancePhoneSuccessResultContract success => new OkObjectResult(success),
                IUpdateInstancePhoneErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}