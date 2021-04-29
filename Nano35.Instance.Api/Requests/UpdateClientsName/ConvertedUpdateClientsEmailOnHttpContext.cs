using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.UpdateClientsName
{
    public class ConvertedUpdateClientsNameOnHttpContext : 
        PipeInConvert
        <UpdateClientsNameHttpBody, 
            IActionResult,
            IUpdateClientsNameRequestContract, 
            IUpdateClientsNameResultContract>
    {
        public ConvertedUpdateClientsNameOnHttpContext(IPipeNode<IUpdateClientsNameRequestContract, IUpdateClientsNameResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(UpdateClientsNameHttpBody input)
        {
            var converted = new UpdateClientsNameRequestContract()
            {
                ClientId = input.ClientId,
                Name = input.Name
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IUpdateClientsNameSuccessResultContract success => new OkObjectResult(success),
                IUpdateClientsNameErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}