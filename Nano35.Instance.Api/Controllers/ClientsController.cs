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
            [FromQuery] Guid clientTypeId, 
            [FromQuery] Guid clientStateId,
            [FromQuery] Guid instanceId)
        {
            var request = new GetAllClientsQuery()
            {
                ClientTypeId = clientTypeId,
                ClientStateId = clientStateId,
                InstanceId = instanceId
            };
            
            var result = await _mediator.Send(request);

            return result switch
            {
                IGetAllClientsSuccessResultContract success => Ok(success.Data),
                IGetAllClientsErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    
        [HttpGet]
        [Route("GetAllClientTypes")]
        public async Task<IActionResult> GetAllClientTypes()
        {
            var request = new GetAllClientTypesQuery();
            
            var result = await _mediator.Send(request);

            return result switch
            {
                IGetAllClientTypesSuccessResultContract success => Ok(success.Data),
                IGetAllClientTypesErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    
        [HttpGet]
        [Route("GetAllClientStates")]
        public async Task<IActionResult> GetAllClientStates()
        {
            var request = new GetAllClientStatesQuery();
            
            var result = await _mediator.Send(request);
            
            return result switch
            {
                IGetAllClientStatesSuccessResultContract success => Ok(success.Data),
                IGetAllClientStatesErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }

        [HttpPost]
        [Route("CreateClient")]
        public async Task<IActionResult> CreateClient(
            [FromBody]CreateClientCommand command)
        {
            var result = await _mediator.Send(command);

            return result switch
            {
                ICreateClientSuccessResultContract => Ok(),
                ICreateClientErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    }
}