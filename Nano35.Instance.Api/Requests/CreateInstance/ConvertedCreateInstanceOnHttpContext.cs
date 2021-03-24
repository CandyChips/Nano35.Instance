using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.CreateInstance
{
    public class ConvertedCreateInstanceOnHttpContext :
        PipeInConvert
        <CreateInstanceHttpBody, 
            IActionResult,
            ICreateInstanceRequestContract, 
            ICreateInstanceResultContract>
        {
        public ConvertedCreateInstanceOnHttpContext(IPipeNode<ICreateInstanceRequestContract, ICreateInstanceResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(CreateInstanceHttpBody input)
        {
            var converted = new CreateInstanceRequestContract()
            {
                Email = input.Email,
                Info = input.Info,
                Name = input.Name,
                RealName = input.RealName,
                NewId = input.NewId,
                Phone = input.Phone,
                RegionId = input.RegionId,
                TypeId = input.TypeId,
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                ICreateInstanceSuccessResultContract success => new OkObjectResult(success),
                ICreateInstanceErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}

