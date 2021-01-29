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
    public class ClientsController : ControllerBase
    {
        private readonly ILogger<ClientsController> _logger;
        private readonly IMediator _mediator;

        public ClientsController(
            ILogger<ClientsController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
    
        [HttpGet]
        [Route("GetAllClients")]
        public async Task<IActionResult> GetAllClients(
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
        [Route("GetAllClientTypes")]
        public async Task<IActionResult> GetAllClientTypes()
        {
            var result = await this._mediator.Send(new GetAllClientTypesQuery());
            if (result is IGetAllClientTypesSuccessResultContract success)
            {
                return Ok(success.Data);
            }
            if (result is IGetAllClientTypesErrorResultContract error)
            {
                return BadRequest(error.Message);
            }
            return BadRequest();
        }
    
        [HttpGet]
        [Route("GetAllClientStates")]
        public async Task<IActionResult> GetAllClientStates()
        {
            var result = await this._mediator.Send(new GetAllClientStatesQuery());
            if (result is IGetAllClientStatesSuccessResultContract success)
            {
                return Ok(success.Data);
            }
            if (result is IGetAllClientStatesErrorResultContract error)
            {
                return BadRequest(error.Message);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("CreateClient")]
        public async Task<IActionResult> CreateClient(
            [FromBody]CreateClientCommand command)
        {
            var result = await this._mediator.Send(command);
            if (result is ICreateClientSuccessResultContract)
            {
                return Ok();
            }
            if (result is ICreateClientErrorResultContract error)
            {
                return BadRequest(error.Message);
            }
            return BadRequest();
        }
    }
}