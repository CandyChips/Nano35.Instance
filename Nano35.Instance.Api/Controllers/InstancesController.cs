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
            
            var result = await _mediator.Send(request);

            return result switch
            {
                IGetAllInstancesSuccessResultContract success => Ok(success.Data),
                IGetAllInstancesErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }

        [HttpGet]
        [Route("GetInstanceById/Id={id}")]
        public async Task<IActionResult> GetInstanceById(
            [FromRoute] Guid id)
        {
            var request = new GetInstanceByIdQuery() {InstanceId = id};
            
            var result = await _mediator.Send(request);

            return result switch
            {
                IGetInstanceByIdSuccessResultContract success => Ok(success.Data),
                IGetInstanceByIdErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }

        [HttpGet]
        [Route("GetAllInstanceTypes")]
        public async Task<IActionResult> GetAllInstanceTypes()
        {
            var request = new GetAllInstanceTypesQuery();
            
            var result = await _mediator.Send(request);

            return result switch
            {
                IGetAllInstanceTypesSuccessResultContract success => Ok(success.Data),
                IGetAllInstanceTypesErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }

        [HttpGet]
        [Route("GetAllRegions")]
        public async Task<IActionResult> GetAllRegions()
        {
            var request = new GetAllRegionsQuery();
            
            var result = await _mediator.Send(request);

            return result switch
            {
                IGetAllRegionsSuccessResultContract success => Ok(success.Data),
                IGetAllRegionsErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }

        [HttpPost]
        [Route("CreateInstance")]
        public async Task<IActionResult> CreateInstance(
            [FromBody]CreateInstanceCommand command)
        {
            var result = await this._mediator.Send(command);
            return result switch
            {
                ICreateInstanceSuccessResultContract => Ok(),
                ICreateInstanceErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    }
}