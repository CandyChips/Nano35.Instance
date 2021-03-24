using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.GetAllClientTypes
{
    public class ConvertedGetAllClientTypesOnHttpContext :
        PipeInConvert
        <GetAllClientTypesHttpQuery, 
            IActionResult,
            IGetAllClientTypesRequestContract, 
            IGetAllClientTypesResultContract>
        {
        public ConvertedGetAllClientTypesOnHttpContext(IPipeNode<IGetAllClientTypesRequestContract, IGetAllClientTypesResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(GetAllClientTypesHttpQuery input)
        {
            var converted = new GetAllClientTypesRequestContract();

            var response = await DoNext(converted);
            
            return response switch
            {
                IGetAllClientTypesSuccessResultContract success => new OkObjectResult(success),
                IGetAllClientTypesErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}

