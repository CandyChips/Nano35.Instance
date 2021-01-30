using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Requests;

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
            
            var result = await _mediator.Send(request);

            return result switch
            {
                IGetAllUnitsSuccessResultContract success => Ok(success.Data),
                IGetAllUnitsErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    
        [HttpGet]
        [Route("GetAllUnitTypes")]
        public async Task<IActionResult> GetAllUnitTypes()
        {
            var request = new GetAllUnitTypesQuery();
            
            var result = await _mediator.Send(request);

            return result switch
            {
                IGetAllUnitTypesSuccessResultContract success => Ok(success.Data),
                IGetAllUnitTypesErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }

        [HttpPost]
        [Route("CreateUnit")]
        public async Task<IActionResult> CreateUnit(
            [FromBody]CreateUnitCommand request)
        {
            var result = await _mediator.Send(request);

            return result switch
            {
                ICreateUnitSuccessResultContract => Ok(),
                ICreateUnitErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    }
}