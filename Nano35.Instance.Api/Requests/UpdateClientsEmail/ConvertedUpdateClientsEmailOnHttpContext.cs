using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.UpdateClientsEmail
{
    public class ConvertedUpdateClientsEmailOnHttpContext : 
        PipeInConvert
        <UpdateClientsEmailHttpBody, 
            IActionResult,
            IUpdateClientsEmailRequestContract, 
            IUpdateClientsEmailResultContract>
    {
        public ConvertedUpdateClientsEmailOnHttpContext(IPipeNode<IUpdateClientsEmailRequestContract, IUpdateClientsEmailResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(UpdateClientsEmailHttpBody input)
        {
            var converted = new UpdateClientsEmailRequestContract()
            {
                ClientId = input.ClientId,
                Email = input.Email
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IUpdateClientsEmailSuccessResultContract success => new OkObjectResult(success),
                IUpdateClientsEmailErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}