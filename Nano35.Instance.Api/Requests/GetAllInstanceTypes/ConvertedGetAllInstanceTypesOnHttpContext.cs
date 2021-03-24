using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.GetAllInstanceTypes
{
    public class ConvertedGetAllInstanceTypesOnHttpContext :
        PipeInConvert
        <GetAllInstanceTypesHttpQuery, 
            IActionResult,
            IGetAllInstanceTypesRequestContract, 
            IGetAllInstanceTypesResultContract>
        {
        public ConvertedGetAllInstanceTypesOnHttpContext(IPipeNode<IGetAllInstanceTypesRequestContract, IGetAllInstanceTypesResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(GetAllInstanceTypesHttpQuery input)
        {
            var converted = new GetAllInstanceTypesRequestContract();

            var response = await DoNext(converted);
            
            return response switch
            {
                IGetAllInstanceTypesSuccessResultContract success => new OkObjectResult(success),
                IGetAllInstanceTypesErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}

