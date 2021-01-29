﻿using System;
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
    public class WorkersController : ControllerBase
    {
        private readonly ILogger<WorkersController> _logger;
        private readonly IMediator _mediator;

        public WorkersController(
            ILogger<WorkersController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
    
        [HttpGet]
        [Route("GetAllWorkers")]
        public async Task<IActionResult> GetAllWorkers(
            [FromQuery] Guid roleId,
            [FromQuery] Guid instanceId)
        {
            var request = new GetAllWorkersQuery()
            {
                InstanceId = instanceId,
                WorkersRoleId = roleId
            };
            var result = await this._mediator.Send(request);
            
            if (result is IGetAllWorkersSuccessResultContract success)
            {
                return Ok(success.Data);
            }
            if (result is IGetAllWorkersErrorResultContract error)
            {
                return BadRequest(error.Message);
            }
            return BadRequest();
        }
    
        [HttpGet]
        [Route("GetAllWorkerRoles")]
        public async Task<IActionResult> GetAllWorkerRoles()
        {
            var request = new GetAllWorkerRolesQuery();
            var result = await this._mediator.Send(request);
            
            if (result is IGetAllWorkerRolesSuccessResultContract success)
            {
                return Ok(success.Data);
            }
            if (result is IGetAllWorkerRolesErrorResultContract error)
            {
                return BadRequest(error.Message);
            }
            return BadRequest();
        }
        
        [HttpPost]
        [Route("CreateWorker")]
        public async Task<IActionResult> CreateWorker(
            [FromBody] CreateWorkerCommand command)
        {
            var result = await this._mediator.Send(command);
            if (result is ICreateWorkerSuccessResultContract)
            {
                return Ok();
            }
            if (result is ICreateWorkerErrorResultContract error)
            {
                return BadRequest(error.Message);
            }
            return BadRequest();
        }
    }
}