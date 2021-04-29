using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.UpdateUnitsWorkingFormat
{
    public class ConvertedUpdateUnitsWorkingFormatOnHttpContext : 
        PipeInConvert
        <UpdateUnitsWorkingFormatHttpBody, 
            IActionResult,
            IUpdateUnitsWorkingFormatRequestContract, 
            IUpdateUnitsWorkingFormatResultContract>
    {
        public ConvertedUpdateUnitsWorkingFormatOnHttpContext(IPipeNode<IUpdateUnitsWorkingFormatRequestContract, IUpdateUnitsWorkingFormatResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(UpdateUnitsWorkingFormatHttpBody input)
        {
            var converted = new UpdateUnitsWorkingFormatRequestContract()
            {
                UnitId = input.UnitId,
                WorkingFormat = input.WorkingFormat
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                IUpdateUnitsWorkingFormatSuccessResultContract success => new OkObjectResult(success),
                IUpdateUnitsWorkingFormatErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}