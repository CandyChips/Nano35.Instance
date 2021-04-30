using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Instance.Artifacts;

namespace Nano35.Instance.Api.Requests.GetInstanceById
{
    public class ConvertedGetInstanceByIdOnHttpContext :
        PipeInConvert
        <Guid, 
            IActionResult,
            IGetInstanceByIdRequestContract, 
            IGetInstanceByIdResultContract>
        {
        public ConvertedGetInstanceByIdOnHttpContext(IPipeNode<IGetInstanceByIdRequestContract, IGetInstanceByIdResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(Guid id) =>
            await DoNext(new GetInstanceByIdRequestContract { InstanceId = id }) switch
            {
                IGetInstanceByIdSuccessResultContract success => new OkObjectResult(success),
                IGetInstanceByIdErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
}

