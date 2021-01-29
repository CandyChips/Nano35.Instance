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
    public class UnitsController : ControllerBase
    {
        private readonly ILogger<UnitsController> _logger;
        private readonly IMediator _mediator;

        public UnitsController(
            ILogger<UnitsController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
    
        [HttpGet]
        [Route("GetAllUnits")]
        public async Task<IActionResult> GetAllUnits(
            [FromQuery] Guid instanceId,
            [FromQuery] Guid unitTypeId)
        {
            var request = new GetAllUnitsQuery()
            {
                InstanceId = instanceId,
                UnitTypeId = unitTypeId
            };
            
            var result = await this._mediator.Send(request);
            
            if (result is IGetAllUnitsSuccessResultContract success)
            {
                return Ok(success.Data);
            }
            if (result is IGetAllUnitsErrorResultContract error)
            {
                return BadRequest(error.Message);
            }
            return BadRequest();
        }
    
        [HttpGet]
        [Route("GetAllUnitTypes")]
        public async Task<IActionResult> GetAllUnitTypes()
        {
            var request = new GetAllUnitTypesQuery();
            
            var result = await this._mediator.Send(request);
            
            if (result is IGetAllUnitTypesSuccessResultContract success)
            {
                return Ok(success.Data);
            }
            
            if (result is IGetAllUnitTypesErrorResultContract error)
            {
                return BadRequest(error.Message);
            }
            
            return BadRequest();
        }

        [HttpPost]
        [Route("CreateUnit")]
        public async Task<IActionResult> CreateUnit(
            [FromBody]CreateUnitCommand request)
        {
            var result = await this._mediator.Send(request);
            
            if (result is ICreateUnitSuccessResultContract)
            {
                return Ok();
            }
            
            if (result is ICreateUnitErrorResultContract error)
            {
                return BadRequest(error.Message);
            }
            
            return BadRequest();
        }
    }
}