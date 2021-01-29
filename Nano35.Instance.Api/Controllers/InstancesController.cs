﻿using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Services.Requests;

namespace Nano35.Instance.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InstancesController : ControllerBase
    {
        private readonly ILogger<InstancesController> _logger;
        private readonly IMediator _mediator;

        public InstancesController(
            ILogger<InstancesController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetAllInstances")]
        public async Task<IActionResult> GetAllInstances(
            [FromQuery] Guid userId, 
            [FromQuery] Guid regionId,
            [FromQuery] Guid instanceTypeId)
        {
            var request = new GetAllInstancesQuery()
            {
                UserId = userId,
                RegionId = regionId,
                InstanceTypeId = instanceTypeId
            };
            var result = await this._mediator.Send(request);
            if (result is IGetAllInstancesSuccessResultContract success)
            {
                return Ok(success.Data);
            }
            if (result is IGetAllInstancesErrorResultContract error)
            {
                return BadRequest(error.Message);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("GetInstanceById/Id={id}")]
        public async Task<IActionResult> GetInstanceById([FromRoute] Guid id)
        {
            var result = await this._mediator.Send(new GetInstanceByIdQuery(id));
            if (result is IGetInstanceByIdSuccessResultContract success)
            {
                return Ok(success.Data);
            }
            if (result is IGetInstanceByIdErrorResultContract error)
            {
                return BadRequest(error.Message);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("GetAllInstanceTypes")]
        public async Task<IActionResult> GetAllInstanceTypes()
        {
            var result = await this._mediator.Send(new GetAllInstanceTypesQuery());
            if (result is IGetAllInstanceTypesSuccessResultContract success)
            {
                return Ok(success.Data);
            }
            if (result is IGetAllInstanceTypesErrorResultContract error)
            {
                return BadRequest(error.Message);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("GetAllRegions")]
        public async Task<IActionResult> GetAllRegions()
        {
            var result = await this._mediator.Send(new GetAllRegionsQuery());
            if (result is IGetAllRegionsSuccessResultContract success)
            {
                return Ok(success.Data);
            }
            if (result is IGetAllRegionsErrorResultContract error)
            {
                return BadRequest(error.Message);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("CreateInstance")]
        public async Task<IActionResult> CreateInstance(
            [FromBody]CreateInstanceCommand command)
        {
            var result = await this._mediator.Send(command);
            if (result is ICreateInstanceSuccessResultContract)
            {
                return Ok();
            }
            if (result is ICreateInstanceErrorResultContract error)
            {
                return BadRequest(error.Message);
            }
            return BadRequest();
        }
    }
}