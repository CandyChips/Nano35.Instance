using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Identity.Artifacts;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.identity;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.UpdateClientsSelle
{
    public class ConvertedUpdateClientsSelleOnHttpContext : 
        PipeInConvert
        <UpdateClientsSelleHttpBody, 
            IActionResult,
            IUpdateClientsSelleRequestContract, 
            IUpdateClientsSelleResultContract>
    {
        public ConvertedUpdateClientsSelleOnHttpContext(IPipeNode<IUpdateClientsSelleRequestContract, IUpdateClientsSelleResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(UpdateClientsSelleHttpBody input)
        {
            var converted = new UpdateClientsSelleRequestContract()
            {
                ClientId = input.ClientId,
                Selle = input.Selle
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IUpdateClientsSelleSuccessResultContract success => new OkObjectResult(success),
                IUpdateClientsSelleErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}