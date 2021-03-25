﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.HttpContext.instance;

namespace Nano35.Instance.Api.Requests.GetAllWorkers
{
    public class ConvertedGetAllWorkersOnHttpContext :
        PipeInConvert
        <GetAllWorkersHttpQuery, 
            IActionResult,
            IGetAllWorkersRequestContract, 
            IGetAllWorkersResultContract>
        {
        public ConvertedGetAllWorkersOnHttpContext(IPipeNode<IGetAllWorkersRequestContract, IGetAllWorkersResultContract> next) : base(next) {}

        public override async Task<IActionResult> Ask(GetAllWorkersHttpQuery input)
        {
            var converted = new GetAllWorkersRequestContract();

            var response = await DoNext(converted);
            
            return response switch
            {
                IGetAllWorkersSuccessResultContract success => new OkObjectResult(success),
                IGetAllWorkersErrorResultContract error => new BadRequestObjectResult(error),
                _ => new BadRequestObjectResult("")
            };
        }
    }
}
