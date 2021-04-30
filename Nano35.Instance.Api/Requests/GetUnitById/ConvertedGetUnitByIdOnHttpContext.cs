using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetUnitById
{
    public class ConvertedGetUnitByIdOnHttpContext :
        PipeInConvert<Guid, IActionResult,
            IGetUnitByIdRequestContract, IGetUnitByIdResultContract>
        {
        public ConvertedGetUnitByIdOnHttpContext(IPipeNode<IGetUnitByIdRequestContract, IGetUnitByIdResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(Guid id) =>
            await DoNext(new GetUnitByIdRequestContract { UnitId = id }) switch
            {
                IGetUnitByIdSuccessResultContract success => new OkObjectResult(success),
                IGetUnitByIdErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
}

