using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.UpdateClientsState
{
    public class ConvertedUpdateClientsStateOnHttpContext : 
        PipeInConvert
        <UpdateClientsStateHttpBody, 
            IActionResult,
            IUpdateClientsStateRequestContract, 
            IUpdateClientsStateResultContract>
    {
        public ConvertedUpdateClientsStateOnHttpContext(IPipeNode<IUpdateClientsStateRequestContract, IUpdateClientsStateResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(UpdateClientsStateHttpBody input)
        {
            var converted = new UpdateClientsStateRequestContract()
            {
                ClientId = input.ClientId,
                StateId = input.StateId
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IUpdateClientsStateSuccessResultContract success => new OkObjectResult(success),
                IUpdateClientsStateErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}