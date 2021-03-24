using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.CreateCashOutput
{
    public class ConvertedCreateCashOutputOnHttpContext :
        PipeInConvert
        <CreateCashOutputHttpBody, 
            IActionResult,
            ICreateCashOutputRequestContract, 
            ICreateCashOutputResultContract>
        {
        public ConvertedCreateCashOutputOnHttpContext(IPipeNode<ICreateCashOutputRequestContract, ICreateCashOutputResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(CreateCashOutputHttpBody input)
        {
            var converted = new CreateCashOutputRequestContract()
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
                ICreateCashOutputSuccessResultContract success => new OkObjectResult(success),
                ICreateCashOutputErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}

