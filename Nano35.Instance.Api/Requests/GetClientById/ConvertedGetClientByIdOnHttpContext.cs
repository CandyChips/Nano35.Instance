using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.GetClientById
{
    public class ConvertedGetClientByIdOnHttpContext :
        PipeInConvert
        <GetClientByIdHttpQuery, 
            IActionResult,
            IGetClientByIdRequestContract, 
            IGetClientByIdResultContract>
        {
        public ConvertedGetClientByIdOnHttpContext(IPipeNode<IGetClientByIdRequestContract, IGetClientByIdResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(GetClientByIdHttpQuery input)
        {
            var converted = new GetClientByIdRequestContract() { UnitId = input.Id };

            var response = await DoNext(converted);
            
            return response switch
            {
                IGetClientByIdSuccessResultContract success => new OkObjectResult(success),
                IGetClientByIdErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}

