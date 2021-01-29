using System;
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
        [Route("GetAllWorkers/ByRole={id}")]
        public async Task<IActionResult> GetAllWorkers(Guid roleId)
        {
            var instanceId = Request.Headers["x-instance-id"];
            if(instanceId.Any() == false) return BadRequest(); //401
            
            var result = await this._mediator.Send(new GetAllWorkersQuery() 
                { InstanceId = Guid.Parse(instanceId), WorkersRoleId = roleId});
            if (result is IGetAllWorkersSuccessResultContract)
            {
                return Ok(result);
            }
            if (result is IGetAllWorkersErrorResultContract)
            {
                return BadRequest("Not found");
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("CreateWorker")]
        public async Task<IActionResult> CreateWorker(
            [FromBody]CreateWorkerCommand command)
        {
            var instanceId = Request.Headers["x-instance-id"];
            if(instanceId.Any() == false) return BadRequest(); //401
            command.InstanceId = Guid.Parse(instanceId);
            
            var newId = Request.Headers["x-new-id"];
            if(newId.Any() == false) return BadRequest(); //401
            command.NewId = Guid.Parse(newId);
            
            var result = await this._mediator.Send(command);
            if (result is ICreateWorkerSuccessResultContract)
            {
                return Ok(result);
            }
            if (result is ICreateWorkerErrorResultContract)
            {
                return BadRequest("bAD DATA");
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("test")]
        public async Task<IActionResult> TestAction()
        {
            return Ok("success");
        }
    }
}