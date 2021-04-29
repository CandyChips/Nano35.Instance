using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.UpdateClientsPhone
{
    public class ConvertedUpdateClientsPhoneOnHttpContext : 
        PipeInConvert
        <UpdateClientsPhoneHttpBody, 
            IActionResult,
            IUpdateClientsPhoneRequestContract, 
            IUpdateClientsPhoneResultContract>
    {
        public ConvertedUpdateClientsPhoneOnHttpContext(IPipeNode<IUpdateClientsPhoneRequestContract, IUpdateClientsPhoneResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(UpdateClientsPhoneHttpBody input)
        {
            var converted = new UpdateClientsPhoneRequestContract()
            {
                ClientId = input.ClientId,
                Phone = input.Phone
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IUpdateClientsPhoneSuccessResultContract success => new OkObjectResult(success),
                IUpdateClientsPhoneErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}