using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.CreateClient
{
    public class ConvertedCreateClientOnHttpContext :
        PipeInConvert
        <CreateClientHttpBody, 
            IActionResult,
            ICreateClientRequestContract, 
            ICreateClientResultContract>
        {
        public ConvertedCreateClientOnHttpContext(IPipeNode<ICreateClientRequestContract, ICreateClientResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(CreateClientHttpBody input) =>
            await DoNext(
                    new CreateClientRequestContract()
                        {Name = input.Name,
                         Email = input.Email,
                         Phone = input.Phone,
                         Selle = input.Selle,
                         InstanceId = input.InstanceId,
                         ClientStateId = input.ClientStateId,
                         ClientTypeId = input.ClientTypeId,
                         NewId = input.NewId}) switch
                {
                    ICreateClientSuccessResultContract success => new OkObjectResult(success),
                    ICreateClientErrorResultContract error => new BadRequestObjectResult(error),
                    _ => new BadRequestObjectResult("")
                };
        }
}

