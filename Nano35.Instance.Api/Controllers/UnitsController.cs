using System;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nano35.Contracts.Instance.Artifacts;
using Nano35.Instance.Api.Helpers;
using Nano35.Instance.Api.Requests;
using Nano35.Instance.Api.Requests.CreateUnit;
using Nano35.Instance.Api.Requests.GetAllClients;
using Nano35.Instance.Api.Requests.GetAllClientTypes;
using Nano35.Instance.Api.Requests.GetAllUnits;
using Nano35.Instance.Api.Requests.GetAllUnitTypes;
using Nano35.Instance.HttpContracts;

namespace Nano35.Instance.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UnitsController : ControllerBase
    {
        private readonly IServiceProvider  _services;

        public UnitsController(
            IServiceProvider services)
        {
            _services = services;
        }
    
        [HttpGet]
        [Route("GetAllUnits")]
        public async Task<IActionResult> GetAllUnits(
            [FromQuery] GetAllUnitsHttpContext query)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<GetAllUnitsLogger>)_services.GetService(typeof(ILogger<GetAllUnitsLogger>));
            
            // Send request to pipeline
            var result =
                await new GetAllUnitsLogger(logger,
                    new GetAllUnitsValidator(
                        new GetAllUnitsRequest(bus))
                ).Ask(query);
            
            // Check response of get all units request
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
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var logger = (ILogger<GetAllUnitTypesLogger>)_services.GetService(typeof(ILogger<GetAllUnitTypesLogger>));
            
            // Send request to pipeline
            var result =
                await new GetAllUnitTypesLogger(logger,
                    new GetAllUnitTypesRequest(bus)
                ).Ask(new GetAllGetAllUnitTypesHttpContext());
            
            // Check response of get all unit types request
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
            [FromBody]CreateUnitHttpContext body)
        {
            // Setup configuration of pipeline
            var bus = (IBus)_services.GetService(typeof(IBus));
            var auth = (ICustomAuthStateProvider) _services.GetService(typeof(ICustomAuthStateProvider));
            var logger = (ILogger<CreateUnitLogger>)_services.GetService(typeof(ILogger<CreateUnitLogger>));
            
            // Send request to pipeline
            var result = 
                await new CreateUnitLogger(logger, 
                    new CreateUnitValidator(
                        new CreateUnitRequest(bus, auth))).Ask(body);

            // Check response of create unit request
            return result switch
            {
                ICreateUnitSuccessResultContract => Ok(),
                ICreateUnitErrorResultContract error => BadRequest(error.Message),
                _ => BadRequest()
            };
        }
    }
}