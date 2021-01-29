using System;
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
            
            var result = await _mediator.Send(request);

            return result switch
            {
                IGetAllWorkersSuccessResultContract success => Ok(success.Data),
                IGetAllWorkersErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    
        [HttpGet]
        [Route("GetAllWorkerRoles")]
        public async Task<IActionResult> GetAllWorkerRoles()
        {
            var request = new GetAllWorkerRolesQuery();
            
            var result = await _mediator.Send(request);

            return result switch
            {
                IGetAllWorkerRolesSuccessResultContract success => Ok(success.Data),
                IGetAllWorkerRolesErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
        
        [HttpPost]
        [Route("CreateWorker")]
        public async Task<IActionResult> CreateWorker(
            [FromBody] CreateWorkerCommand command)
        {
            var result = await _mediator.Send(command);
            
            return result switch
            {
                ICreateWorkerSuccessResultContract => Ok(),
                ICreateWorkerErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    }
}