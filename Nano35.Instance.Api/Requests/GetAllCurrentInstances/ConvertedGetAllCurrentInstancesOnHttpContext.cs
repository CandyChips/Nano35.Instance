﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.GetAllCurrentInstances
{
    public class ConvertedGetAllCurrentInstancesOnHttpContext :
        PipeInConvert
        <GetAllInstancesHttpQuery, 
            IActionResult,
            IGetAllInstancesRequestContract, 
            IGetAllInstancesResultContract>
        {
        public ConvertedGetAllCurrentInstancesOnHttpContext(IPipeNode<IGetAllInstancesRequestContract, IGetAllInstancesResultContract> next) : base(next) {}
        public override async Task<IActionResult> Ask(GetAllInstancesHttpQuery input) =>
            await DoNext(new GetAllInstancesRequestContract { InstanceTypeId = input.InstanceTypeId, RegionId = input.RegionId, UserId = input.UserId }) switch
                {
                    IGetAllInstancesSuccessResultContract success => new OkObjectResult(success),
                    IGetAllInstancesErrorResultContract error => new BadRequestObjectResult(error),
                    _ => new BadRequestObjectResult("")
                };
        }
}

