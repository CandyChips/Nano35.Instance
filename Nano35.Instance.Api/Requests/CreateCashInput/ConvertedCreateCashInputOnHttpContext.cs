using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.CreateCashInput
{
    public class ConvertedCreateCashInputOnHttpContext :
        PipeInConvert
        <CreateCashInputHttpBody, 
            IActionResult,
            ICreateCashInputRequestContract, 
            ICreateCashInputResultContract>
        {
        public ConvertedCreateCashInputOnHttpContext(IPipeNode<ICreateCashInputRequestContract, ICreateCashInputResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(CreateCashInputHttpBody input)
        {
            var converted = new CreateCashInputRequestContract()
            {
                Cash = input.Cash,
                Description = input.Description,
                InstanceId = input.InstanceId,
                NewId = input.NewId,
                UnitId = input.UnitId,
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                ICreateCashInputSuccessResultContract success => new OkObjectResult(success),
                ICreateCashInputErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}

