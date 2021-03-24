using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.CreateUnit
{
    public class ConvertedCreateUnitOnHttpContext :
        PipeInConvert
        <CreateUnitHttpBody, 
            IActionResult,
            ICreateUnitRequestContract, 
            ICreateUnitResultContract>
        {
        public ConvertedCreateUnitOnHttpContext(IPipeNode<ICreateUnitRequestContract, ICreateUnitResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(CreateUnitHttpBody input)
        {
            var converted = new CreateUnitRequestContract()
            {
                Address = input.Address,
                Id = input.Id,
                InstanceId = input.InstanceId,
                Name = input.Name,
                Phone = input.Phone,
                UnitTypeId = input.UnitTypeId,
                WorkingFormat = input.WorkingFormat
            };

            var response = await DoNext(converted);
            
            return response switch
            {
                ICreateUnitSuccessResultContract success => new OkObjectResult(success),
                ICreateUnitErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}

