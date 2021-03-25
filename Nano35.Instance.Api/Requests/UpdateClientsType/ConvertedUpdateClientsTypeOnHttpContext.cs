using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.UpdateClientsType
{
    public class ConvertedUpdateClientsTypeOnHttpContext : 
        PipeInConvert
        <UpdateClientsTypeHttpBody, 
            IActionResult,
            IUpdateClientsTypeRequestContract, 
            IUpdateClientsTypeResultContract>
    {
        public ConvertedUpdateClientsTypeOnHttpContext(IPipeNode<IUpdateClientsTypeRequestContract, IUpdateClientsTypeResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(UpdateClientsTypeHttpBody input)
        {
            var converted = new UpdateClientsTypeRequestContract()
            {
                ClientId = input.ClientId,
                TypeId = input.TypeId
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IUpdateClientsTypeSuccessResultContract success => new OkObjectResult(success),
                IUpdateClientsTypeErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}