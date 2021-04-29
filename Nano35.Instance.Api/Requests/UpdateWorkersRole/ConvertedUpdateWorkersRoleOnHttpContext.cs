using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.UpdateWorkersRole
{
    public class ConvertedUpdateWorkersRoleOnHttpContext : 
        PipeInConvert
        <UpdateWorkersRoleHttpBody, 
            IActionResult,
            IUpdateWorkersRoleRequestContract, 
            IUpdateWorkersRoleResultContract>
    {
        public ConvertedUpdateWorkersRoleOnHttpContext(IPipeNode<IUpdateWorkersRoleRequestContract, IUpdateWorkersRoleResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(UpdateWorkersRoleHttpBody input)
        {
            var converted = new UpdateWorkersRoleRequestContract()
            {
                WorkersId = input.WorkersId,
                RoleId = input.RoleId
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IUpdateWorkersRoleSuccessResultContract success => new OkObjectResult(success),
                IUpdateWorkersRoleErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}