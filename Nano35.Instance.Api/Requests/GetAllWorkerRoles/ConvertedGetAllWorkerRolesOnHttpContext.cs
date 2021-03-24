using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.GetAllWorkerRoles
{
    public class ConvertedGetAllWorkerRolesOnHttpContext :
        PipeInConvert
        <GetAllWorkerRolesHttpQuery, 
            IActionResult,
            IGetAllWorkerRolesRequestContract, 
            IGetAllWorkerRolesResultContract>
        {
        public ConvertedGetAllWorkerRolesOnHttpContext(IPipeNode<IGetAllWorkerRolesRequestContract, IGetAllWorkerRolesResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(GetAllWorkerRolesHttpQuery input)
        {
            var converted = new GetAllWorkerRolesRequestContract();

            var response = await DoNext(converted);
            
            return response switch
            {
                IGetAllWorkerRolesSuccessResultContract success => new OkObjectResult(success),
                IGetAllWorkerRolesErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}

